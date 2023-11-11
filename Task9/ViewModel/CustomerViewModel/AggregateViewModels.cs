using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9.ViewModel.CustomerViewModel
{
    public class AggregateViewModels
    {
        public AddCustomerViewModel AddCustomerViewModel { get; }
        public UpdateCustomerViewModel UpdateCustomerViewModel { get; }
        public GetAllCustomerViewModel GetAllCustomerViewModel { get; }
        public SharedData SharedData { get; }
        public AggregateViewModels()
        {
            AddCustomerViewModel = new AddCustomerViewModel();
            UpdateCustomerViewModel = new UpdateCustomerViewModel();
            GetAllCustomerViewModel = new GetAllCustomerViewModel();
            SharedData = new SharedData();
        }
    }
}
