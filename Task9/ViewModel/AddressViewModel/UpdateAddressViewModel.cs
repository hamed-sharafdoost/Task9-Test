using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Task9.Commands;
using Task9.Model.DataAccess;
using Task9.Model.DataAccess.Repositories;
using Task9.Model.Entities;

namespace Task9.ViewModel.AddressViewModel
{
    public class UpdateAddressViewModel
    {
        public ObservableCollection<Addresses> AddressList { get; } = new ObservableCollection<Addresses>();
        public DelegateCommand UpdateCommand { get; }
        private Customers selectedCustomer;
        private Addresses selectedAddress;    
        private ConnectionProvider connection;
        private AddressRepository addressRepository;
        public UpdateAddressViewModel()
        {
            connection = new ConnectionProvider();
            addressRepository = new AddressRepository(connection);
            UpdateCommand = new DelegateCommand(UpdateAsync, CanExecute);
        }
        private bool CanExecute(object param) => SelectedAddress != null && SelectedCustomer != null;
        public Addresses SelectedAddress
        {
            get { return selectedAddress; }
            set
            {
                selectedAddress = value;
                if(selectedAddress != null)
                {
                    UpdateCommand.RaiseCanExecuteChangedEvent();
                }
                else
                {
                    UpdateCommand.RaiseCanExecuteChangedEvent();
                }
            }
        }
        public Customers SelectedCustomer
        {
            get { return selectedCustomer; }
            set
            {
                selectedCustomer = value;
                AddressList.Clear();
                if(selectedCustomer != null)
                {
                    foreach(var item in addressRepository.GetAddressesAsync(selectedCustomer.CustomerID).Result)
                    {
                        AddressList.Add(item);
                    }
                }
                else
                {
                    UpdateCommand.RaiseCanExecuteChangedEvent();
                }
            }
        }
        private async void UpdateAsync(object param)
        {
            if(SelectedCustomer != null && SelectedAddress != null)
            {
                await addressRepository.UpdateAsync(SelectedAddress.AddressID, SelectedCustomer.CustomerID);
            }
        }
    }
}
