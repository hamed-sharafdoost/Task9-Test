using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Model.DataAccess;
using Task9.Model.DataAccess.Repositories;

namespace Task9.ViewModel.AddressViewModel
{
    public class AggregateAddressViewModels
    {
        public AddAddressViewModel AddAddressViewModel { get; }
        public UpdateAddressViewModel UpdateAddressViewModel { get; }
        public GetAllAddressViewModel GetAllAddressViewModel { get; }
        public SharedData SharedData { get; }
        private ConnectionProvider connection;
        private CustomerRepository repository;
        public AggregateAddressViewModels()
        {
            SharedData.CustomerList.Clear();
            connection = new ConnectionProvider();
            repository = new CustomerRepository(connection);
            SharedData = new SharedData();
            loadCustomersAsync();
            AddAddressViewModel = new AddAddressViewModel();
            UpdateAddressViewModel = new UpdateAddressViewModel();
            GetAllAddressViewModel = new GetAllAddressViewModel();
        }
        private async void loadCustomersAsync()
        {
            foreach (var item in await repository.GetAll())
            {
                SharedData.CustomerList.Add(item);
            }
        }
    }
}
