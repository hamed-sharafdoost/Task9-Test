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
    public class GetByDateViewModel : ValidationViewModelBase
    {
        public DelegateCommand GetByDateCommand { get; }
        private DateTime endDate = DateTime.Now;
        private DateTime startDate =DateTime.Now;
        private ConnectionProvider connection;
        private CustomerOrderRepository orderRepository;
        private CustomerProductRepository orderProductRepository;
        public GetByDateViewModel()
        {
            connection = new ConnectionProvider();
            orderRepository = new CustomerOrderRepository(connection);
            orderProductRepository = new CustomerProductRepository(connection);
            GetByDateCommand = new DelegateCommand(getOrdersAsync);
        }
        public bool CanExecute(object param) => !HasErrors;
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                if(endDate.CompareTo(StartDate) < 0)
                {
                    AddError("End date is less than start date",nameof(EndDate));
                    GetByDateCommand.RaiseCanExecuteChangedEvent();
                }
                else
                {
                    ClearErrors(nameof(EndDate));
                    GetByDateCommand.RaiseCanExecuteChangedEvent();
                }
            }
        }
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                if(startDate.CompareTo(EndDate) > 0)
                {
                    AddError("StartDate is greater than end date", nameof(StartDate));
                    GetByDateCommand.RaiseCanExecuteChangedEvent();
                }
                else
                {
                    ClearErrors(nameof(StartDate));
                    GetByDateCommand.RaiseCanExecuteChangedEvent();
                }
            }
        }
        private async void getOrdersAsync(object param)
        {
            if(StartDate == null || EndDate == null) { return; }
            SharedData.Orders.Clear();
            IEnumerable<CustomerOrdersProducts> orderProduct = orderProductRepository.GetAll();
            IEnumerable<Products> products = SharedData.ProductList;
            string orderStatus = string.Empty;
            foreach (var order in await orderRepository.GetAllAsync(StartDate,EndDate))
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
