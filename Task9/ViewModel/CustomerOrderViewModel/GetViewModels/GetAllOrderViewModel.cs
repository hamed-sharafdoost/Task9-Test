using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Commands;
using Task9.Model.DataAccess;
using Task9.Model.DataAccess.Repositories;
using Task9.Model.Entities;

namespace Task9.ViewModel.CustomerOrderViewModel.GetViewModels
{
    public class GetAllOrderViewModel
    {
        public DelegateCommand GetAllCommand { get; }
        private ConnectionProvider connection;
        private CustomerOrderRepository orderRepository;
        private CustomerProductRepository orderProductRepository;
        public GetAllOrderViewModel()
        {
            connection = new ConnectionProvider();
            orderRepository = new CustomerOrderRepository(connection);
            orderProductRepository = new CustomerProductRepository(connection);
            GetAllCommand = new DelegateCommand(getAllOrdersAsync);
        }
        private async void getAllOrdersAsync(object param)
        {
            SharedData.Orders.Clear();
            IEnumerable<CustomerOrdersProducts> orderProduct = orderProductRepository.GetAll();
            IEnumerable<Products> products = SharedData.ProductList;
            string orderStatus = string.Empty;
            foreach(var order in await orderRepository.GetAllAsync())
            {
                switch (order.OrderStatusCode)
                {
                    case 1:
                        orderStatus = "Shopping";
                        break;
                    case 2:
                        orderStatus = "PayOrder";
                        break;
                    case 3:
                        orderStatus = "Packing";
                        break;
                    case 4:
                        orderStatus = "Delivery";
                        break;
                }
                CustomizedOrder customizedOrder = new CustomizedOrder();
                customizedOrder.CustomerName = SharedData.CustomerList.First(b => b.CustomerID == order.CustomerID).Username;
                customizedOrder.DateOrderPlaced = order.DateOrderPlaced;
                customizedOrder.Price = order.OrderPrice;
                customizedOrder.OrderStatus = orderStatus;
                customizedOrder.Paymentmethod = order.PaymentMethodID == 1 ? "OnSpot" : "Online";
                foreach(var product in products)
                {
                    foreach(var orderproduct in orderProduct)
                    {
                        if(order.OrderID == orderproduct.OrderID)
                        {
                            if(orderproduct.ProductID == product.ProductID)
                            {
                                customizedOrder.Products.Add(product.Name);
                                customizedOrder.Amount.Add(orderproduct.Quantity);
                            }
                        }
                    }
                }
                SharedData.Orders.Add(customizedOrder);
            }
        }
    }
}
