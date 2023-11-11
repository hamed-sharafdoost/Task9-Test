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

namespace Task9.ViewModel.CustomerViewModel
{
    public class GetAllCustomerViewModel
    {
        public ObservableCollection<Customers> CustomerList { get; }
        public DelegateCommand GetAllCommand { get; }
        private ConnectionProvider connection;
        private CustomerRepository repo;
        public GetAllCustomerViewModel()
        {
            connection = new ConnectionProvider();
            repo = new CustomerRepository(connection);
            CustomerList = new ObservableCollection<Customers>();
            GetAllCommand = new DelegateCommand(GetAllAsync);
        }
        private async void GetAllAsync(object param)
        {
            CustomerList.Clear();
            SharedData.customers.Clear();
            foreach(var item in await repo.GetAll())
            {
                SharedData.customers.Add(item);
            }
            foreach(var item in SharedData.customers)
            {
                if(!CustomerList.Contains(item))
                {
                    CustomerList.Add(item);
                }
            }
        }
    }
}
