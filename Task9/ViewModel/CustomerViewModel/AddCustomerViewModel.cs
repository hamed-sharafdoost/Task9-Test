using Task9.Commands;
using Task9.Model.DataAccess;
using Task9.Model.Entities;
using Task9.Model.DataAccess.Repositories;
using System.Linq;
using System.Collections.Generic;

namespace Task9.ViewModel.CustomerViewModel
{
    public class AddCustomerViewModel : ValidationViewModelBase
    {
        private string userName;
        private string name;
        private string phone;
        private string email;
        public DelegateCommand AddCustomerCommand { get; }
        public List<Customers> List { get; } = new List<Customers>();
        private CustomerRepository repository;
        private ConnectionProvider connection; 
        public AddCustomerViewModel()
        {
            connection = new ConnectionProvider();
            repository = new CustomerRepository(connection);
            loadCustomersAsync();
            AddCustomerCommand = new DelegateCommand(AddCustomer, canExecute);
        }
        private async void loadCustomersAsync()
        {
            foreach (var item in await repository.GetAll())
            {
                List.Add(item);
            }
        }
        private bool canExecute(object arg) => !HasErrors;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                if (!string.IsNullOrEmpty(userName))
                {
                    Customers customer = List.FirstOrDefault(b => b.Username == userName);
                    if (customer != null)
                    {
                        AddError("UserName is already existed", nameof(UserName));
                        AddCustomerCommand.RaiseCanExecuteChangedEvent();
                    }
                    else
                    {
                        ClearErrors(nameof(UserName));
                        AddCustomerCommand.RaiseCanExecuteChangedEvent();
                    }
                }
                else
                {
                    AddError("Username is needed",nameof(UserName));
                    AddCustomerCommand.RaiseCanExecuteChangedEvent();
                }
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                if(string.IsNullOrEmpty(name))
                {
                    AddError("Name is empty", nameof(Name));
                    AddCustomerCommand.RaiseCanExecuteChangedEvent();
                }
                else
                {
                    ClearErrors(nameof(Name));
                    AddCustomerCommand.RaiseCanExecuteChangedEvent();
                }
            }
        }
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                if (string.IsNullOrEmpty(phone))
                {
                    AddError("Phone is empty", nameof(Phone));
                    AddCustomerCommand.RaiseCanExecuteChangedEvent();
                }
                else
                {
                    ClearErrors(nameof(Phone));
                    AddCustomerCommand.RaiseCanExecuteChangedEvent();
                }
            }
        }
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                if (string.IsNullOrEmpty(email))
                {
                    AddError("Email is empty", nameof(Email));
                    AddCustomerCommand.RaiseCanExecuteChangedEvent();
                }
                else
                {
                    ClearErrors(nameof(Email));
                    AddCustomerCommand.RaiseCanExecuteChangedEvent();
                }
            }
        }
        private void AddCustomer(object param)
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(Email))
            {
                AddCustomerCommand.RaiseCanExecuteChangedEvent();
                AddError("All fields have to be filled", "All");
            }
            else
            {
                ClearErrors("All");
                SharedData.customers.Add(new Customers { Username = UserName, Name = Name, Phone = Phone, Email = Email });
                repository.AddCustomer(new Customers { Username = UserName, Name = Name, Phone = Phone, Email = Email });
            }
        }
    }
}
