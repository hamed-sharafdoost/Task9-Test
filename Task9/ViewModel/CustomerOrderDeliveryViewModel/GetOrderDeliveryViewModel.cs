using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Commands;
using Task9.Model.DataAccess;
using Task9.Model.DataAccess.Repositories;
using Task9.ViewModel.AddressViewModel;

namespace Task9.ViewModel.CustomerOrderDeliveryViewModel
{
    public class GetOrderDeliveryViewModel
    {
        public ObservableCollection<CustomizedOrderDelivery> CustomizedOrders { get; } = new ObservableCollection<CustomizedOrderDelivery>();
        public DelegateCommand GetDeliveryCommand { get; set; }
        private ConnectionProvider connection;
        private CustomerOrdersDeliveryRepository deliveryRepository;
        private CustomerOrderRepository customerOrderRepository;
        public GetOrderDeliveryViewModel()
        {
            connection = new ConnectionProvider();
            deliveryRepository = new CustomerOrdersDeliveryRepository(connection);
            customerOrderRepository = new CustomerOrderRepository(connection);
            GetDeliveryCommand = new DelegateCommand(getOrderDeliveryAsync);
        }
        private async void getOrderDeliveryAsync(object param)
        {
            CustomizedOrders.Clear();
            foreach(var item in await deliveryRepository.GetAllAsync())
            {
                foreach(var order in await customerOrderRepository.GetAllAsync())
                {
                    if(item.OrderID == order.OrderID)
                    {
                        CustomizedOrders.Add(new CustomizedOrderDelivery
                        {
                            CustomerName = SharedDataDelivery.CustomerList.First(b => b.CustomerID == order.CustomerID).Username,
                            OrderPrice = order.OrderPrice,
                            DateOrderPlaced = order.DateOrderPlaced,
                            DeliveryStatus = item.DeliveryStatusCode == 1 ? "Sending" : "Delivered"
                        });
                    }
                }
            }
        }
    }
}
