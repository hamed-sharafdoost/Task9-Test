using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Model.DataAccess;
using Task9.Model.DataAccess.Repositories;
using Task9.ViewModel.AddressViewModel;

namespace Task9.ViewModel.CustomerOrderDeliveryViewModel
{
    public class AggregateDeliveryViewModels
    {
        public AddOrderDeliveryViewModel AddDeliveryModel { get; }
        public GetOrderDeliveryViewModel GetDeliveryModel { get; }
        public SharedDataDelivery SharedData { get; }
        private ConnectionProvider connection;
        private CustomerRepository customerRepository;
        public AggregateDeliveryViewModels()
        {
           SharedDataDelivery.CustomerList.Clear();
            connection = new ConnectionProvider();
            customerRepository = new CustomerRepository(connection);
            AddDeliveryModel = new AddOrderDeliveryViewModel();
            GetDeliveryModel = new GetOrderDeliveryViewModel();
            SharedData = new SharedDataDelivery();
            loadCustomersAsync();
        }
        private async void loadCustomersAsync()
        {
            foreach (var item in await customerRepository.GetAll())
            {
                SharedDataDelivery.CustomerList.Add(item);
            }
        }
    }
}
