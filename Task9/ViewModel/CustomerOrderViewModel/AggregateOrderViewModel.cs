using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Model.DataAccess;
using Task9.Model.DataAccess.Repositories;
using Task9.ViewModel.CustomerOrderViewModel.GetViewModels;

namespace Task9.ViewModel.CustomerOrderViewModel
{
    public class AggregateOrderViewModel
    {
        public AddCustomerOrderViewModel AddViewModel { get;}
        public GetAllOrderViewModel AllOrderViewModel { get; }
        public UpdateCustomerOrderViewModel UpdateViewModel { get; }
        public DeleteCustomerOrderViewModel DeleteViewModel { get; }
        public GetByDateViewModel GetByDateViewModel { get; }
        public GetOrderByProduct GetOrderByProduct { get; }
        public GetOrderByPriceViewModel GetOrderByPrice { get; }
        public SharedData SharedData { get; set;}
        private ConnectionProvider connection;
        private CustomerRepository customerRepository;
        private ProductRepository productRepository;
        public AggregateOrderViewModel()
        {
            SharedData.ProductList.Clear();
            SharedData.CustomerList.Clear();
            SharedData.Orders.Clear();
            AddViewModel = new AddCustomerOrderViewModel();
            AllOrderViewModel = new GetAllOrderViewModel();
            UpdateViewModel = new UpdateCustomerOrderViewModel();
            DeleteViewModel = new DeleteCustomerOrderViewModel();
            GetByDateViewModel = new GetByDateViewModel();
            GetOrderByProduct = new GetOrderByProduct();
            GetOrderByPrice = new GetOrderByPriceViewModel();
            SharedData = new SharedData();
            connection = new ConnectionProvider();
            productRepository = new ProductRepository(connection);
            customerRepository = new CustomerRepository(connection);
            loadCustomersAsync();
            loadProductsAsync();
        }
        private async void loadCustomersAsync()
        {
            foreach (var item in await customerRepository.GetAll())
            {
                SharedData.CustomerList.Add(item);
            }
        }
        private async void loadProductsAsync()
        {
            foreach (var item in await productRepository.GetAllAsync())
            {
                SharedData.ProductList.Add(item);
            }
        }
    }
}
