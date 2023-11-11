using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Commands;
using Task9.Model.DataAccess;
using Task9.Model.DataAccess.Repositories;
using Task9.Model.Entities;

namespace Task9.ViewModel.CustomerOrderViewModel.GetViewModels
{
    public class GetOrderByPriceViewModel : ValidationViewModelBase
    {
        public DelegateCommand GetOrderCommand { get; }
        public bool Upper { get; set; } = true;
        private int price;
        private ConnectionProvider connection;
        private CustomerOrderRepository orderRepository;
        private CustomerProductRepository orderProductRepository;
        public GetOrderByPriceViewModel()
        {
            connection = new ConnectionProvider();
            orderRepository = new CustomerOrderRepository(connection);
            orderProductRepository = new CustomerProductRepository(connection);
            GetOrderCommand = new DelegateCommand(getAllOrdersAsync,CanExecute);
        }
        public bool CanExecute(object param) => !HasErrors;
        public int Price
        {
            get { return price; }
            set
            {
                price = value;
                if(price == 0)
                {
                    AddError("Price must be filled", nameof(Price));
                    GetOrderCommand.RaiseCanExecuteChangedEvent();
                }
                else
                {
                    ClearErrors(nameof(Price));
                    GetOrderCommand.RaiseCanExecuteChangedEvent();
                }
            }
        }
        private async void getAllOrdersAsync(object param)
        {
            SharedData.Orders.Clear();
            IEnumerable<CustomerOrdersProducts> orderProduct = orderProductRepository.GetAll();
            IEnumerable<Products> products = SharedData.ProductList;
            string orderStatus = string.Empty;
            foreach (var order in await orderRepository.GetAllAsync(Price,Upper))
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
                foreach (var product in products)
                {
                    foreach (var orderproduct in orderProduct)
                    {
                        if (order.OrderID == orderproduct.OrderID)
                        {
                            if (orderproduct.ProductID == product.ProductID)
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
