using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task9.Commands;
using Task9.Model.DataAccess;
using Task9.Model.DataAccess.Repositories;
using Task9.Model.Entities;

namespace Task9.ViewModel.AddressViewModel
{
    public class AddAddressViewModel : ValidationViewModelBase
    {
        public Customers SelectedItem { get; set; }
        public DelegateCommand AddAddressCommand { get; }
        private string city;
        private int postCode;
        private string address;
        private ConnectionProvider connection;
        private AddressRepository addressRepository;
        public AddAddressViewModel()
        {
            connection = new ConnectionProvider();
            addressRepository = new AddressRepository(connection);
            AddAddressCommand = new DelegateCommand(AddAddressAsync,CanExecute);
        }
        public bool CanExecute(object param) => !HasErrors;
        public string City
        {
            get { return city; }
            set
            {
                city = value;
                if(string.IsNullOrEmpty(city))
                {
                    AddError("City is empty",nameof(City));
                    AddAddressCommand.RaiseCanExecuteChangedEvent();
                }
                else
                {
                    ClearErrors(nameof(City));
                    AddAddressCommand.RaiseCanExecuteChangedEvent();
                }
            }
        }
        public int PostCode
        {
            get { return postCode; }
            set
            {
                postCode = value;
                if(postCode == 0)
                {
                    AddError("PostCode is empty", nameof(PostCode));
                    AddAddressCommand.RaiseCanExecuteChangedEvent();
                }
                else
                {
                    ClearErrors(nameof(PostCode));
                    AddAddressCommand.RaiseCanExecuteChangedEvent();
                }
            }
        }
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                if (string.IsNullOrEmpty(address))
                {
                    AddError("Address is empty", nameof(Address));
                    AddAddressCommand.RaiseCanExecuteChangedEvent();
                }
                else
                {
                    ClearErrors(nameof(Address));
                    AddAddressCommand.RaiseCanExecuteChangedEvent();
                }
            }
        }
        private async void AddAddressAsync(object param)
        {
            if(string.IsNullOrEmpty(City) || postCode == 0 || string.IsNullOrEmpty(Address))
            {
                AddError("All fields have to be filled", "All");
                AddAddressCommand.RaiseCanExecuteChangedEvent();
            }
            else
            {
                ClearAllErrors();
                AddAddressCommand.RaiseCanExecuteChangedEvent();
                await addressRepository.AddNewAddressAsync(new Addresses { City = City, PostCode = PostCode, CompleteAddress = Address }, SelectedItem.CustomerID);
            }
        }
    }
}
