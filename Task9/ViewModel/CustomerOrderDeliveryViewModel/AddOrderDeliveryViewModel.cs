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

namespace Task9.ViewModel.CustomerOrderDeliveryViewModel
{
    public class AddOrderDeliveryViewModel : ValidationViewModelBase
    {
        public ObservableCollection<int> Orders { get; } = new ObservableCollection<int>();
        public DelegateCommand AddOrderDeliveryCommand { get; }
        public int SelectedOrder { get; set; }
        public bool IsDelivered { get; set; } = true;
        private bool selectOrderIsEnabled;
        private Customers selectedUser;
        private ConnectionProvider connection;
        private CustomerOrderRepository orderRepository;
        private CustomerOrdersDeliveryRepository deliveryRepository;
        public AddOrderDeliveryViewModel()
        {
            connection = new ConnectionProvider();
            orderRepository = new CustomerOrderRepository(connection);
            deliveryRepository = new CustomerOrdersDeliveryRepository(connection);
            AddOrderDeliveryCommand = new DelegateCommand(addOrderDeliveryAsync);
        }
        public bool SelectOrderIsEnabled
        {
            get { return selectOrderIsEnabled; }
            set
            {
                selectOrderIsEnabled = value;
                OnPropertyChanged();
            }
        }
        public Customers SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                if(selectedUser != null)
                {
                    getOrdersAsync(SelectedUser.CustomerID);
                    if(Orders.Any())
                    {
                        SelectOrderIsEnabled = true;
                        ClearErrors(nameof(SelectedUser));
                    }
                    else
                    {
                        SelectOrderIsEnabled = false;
                        AddError("This user has no orders", nameof(SelectedUser));
                    }
                }
            }
        }
        private async void getOrdersAsync(int customerId)
        {
            Orders.Clear();
            foreach (var item in await orderRepository.GetOrderIDAsync(customerId))
            {
                Orders.Add(item);
            }
        }
        private async void addOrderDeliveryAsync(object param)
        {
            if(SelectedOrder != 0)
            {
                await deliveryRepository.AddAsync(new CustomerOrdersDelivery
                {
                    OrderID = SelectedOrder,
                    DateReported = DateTime.UtcNow,
                    DeliveryStatusCode = IsDelivered ? 2 : 1
                });
            }
        }
    }
}
