using Task9.Commands;
using Task9.Model.DataAccess;
using Task9.Model.DataAccess.Repositories;
using Task9.Model.Entities;

namespace Task9.ViewModel.CustomerViewModel
{
    public class UpdateCustomerViewModel : ValidationViewModelBase
    {
        private Customers selectedItem;
        public DelegateCommand UpdateCommand { get; }
        private string name;
        private string phone;
        private string email;
        private ConnectionProvider connection;
        private CustomerRepository repository;
        public UpdateCustomerViewModel()
        {
            connection = new ConnectionProvider();
            repository =  new CustomerRepository(connection);
            UpdateCommand = new DelegateCommand(Update);
        }
        public Customers SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                Name = selectedItem.Name;
                Phone = selectedItem.Phone;
                Email = selectedItem.Email;
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
                }
                else
                {
                    ClearErrors(nameof(Name));
                }
                OnPropertyChanged();
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
                }
                else
                {
                    ClearErrors(nameof(Phone));
                }
                OnPropertyChanged();
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
                }
                else
                {
                    ClearErrors(nameof(Email));
                }
                OnPropertyChanged();
            }
        }
        private void Update(object param)
        {
            if(string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(Email))
            {
                AddError("All fields have to be filled", "All");
            }
            else
            {
                ClearErrors("All");
                repository.UpdateCustomer(new Customers
                {
                    CustomerID = SelectedItem.CustomerID,
                    Username = SelectedItem.Username,
                    Name = Name,
                    Phone = Phone,
                    Email = Email
                });
            }    
        }
    }
}
