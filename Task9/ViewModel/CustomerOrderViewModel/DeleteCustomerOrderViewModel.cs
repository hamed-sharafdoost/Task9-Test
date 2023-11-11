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

namespace Task9.ViewModel.CustomerOrderViewModel
{
    public class DeleteCustomerOrderViewModel : ValidationViewModelBase
    {
        public ObservableCollection<int> Orders { get; } = new ObservableCollection<int>();
        public DelegateCommand DeleteCommand { get; }
        public int SelectedOrder { get; set; }
        private Customers selectedUser;
        private bool selectOrderIsEnabled;
        private ConnectionProvider connection;
        private CustomerOrderRepository orderRepository;
        public DeleteCustomerOrderViewModel()
        {
            DeleteCommand = new DelegateCommand(deleteOrderAsync);
            connection = new ConnectionProvider();
            orderRepository = new CustomerOrderRepository(connection);
        }
        public Customers SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                Orders.Clear();
                if(selectedUser != null)
                {
                    getOrdersAsync(selectedUser.CustomerID);
                    if(Orders.Any())
                    {
                        SelectOrderIsEnabled = true;
                    }
                    else
                    {
                        SelectOrderIsEnabled = false;
                    }
                }
            }
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
        private async void deleteOrderAsync(object param)
        {
            if(SelectedOrder != 0)
            {
                await orderRepository.DeleteAsync(SelectedOrder);
            }
        }
        private async void getOrdersAsync(int customerId)
        {
            foreach(var item in await orderRepository.GetOrderIDAsync(customerId))
            {
                Orders.Add(item);
            }
        }
    }
}
