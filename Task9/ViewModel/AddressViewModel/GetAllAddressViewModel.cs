using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Task9.Commands;
using Task9.Model.DataAccess;
using Task9.Model.DataAccess.Repositories;
using Task9.Model.Entities;

namespace Task9.ViewModel.AddressViewModel
{
    public class GetAllAddressViewModel
    {
        public ObservableCollection<AddressCustomerAddress> AddressCustomerList { get; } = new ObservableCollection<AddressCustomerAddress>();
        public Customers SelectedItem { get; set; }
        public DelegateCommand GetAllCommand { get; }
        private ConnectionProvider connection;
        private AddressRepository addressRepository;
        private CustomerRepository customerRepository;
        private CustomerAddressRepository customerAddressRepository;
        public GetAllAddressViewModel()
        {
            connection = new ConnectionProvider();
            addressRepository = new AddressRepository(connection);
            customerRepository = new CustomerRepository(connection);
            customerAddressRepository = new CustomerAddressRepository(connection);
            GetAllCommand = new DelegateCommand(GetAllAsync);
        }
        private async void GetAllAsync(object param)
        {
            AddressCustomerList.Clear();
            if (SelectedItem != null)
            {
                foreach (var address in await addressRepository.GetAddressesAsync(SelectedItem.CustomerID))
                {
                    AddressCustomerList.Add(new AddressCustomerAddress
                    {
                        Username = SelectedItem.Username,
                        City = address.City,
                        PostCode = address.PostCode,
                        CompleteAddress = address.CompleteAddress,
                        DateFrom = customerAddressRepository.Get(address.AddressID, SelectedItem.CustomerID).DateFrom
                    });
                }
            }
        }
    }
}
