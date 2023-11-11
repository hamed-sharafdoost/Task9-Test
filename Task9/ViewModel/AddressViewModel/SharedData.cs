using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Model.Entities;

namespace Task9.ViewModel.AddressViewModel
{
    public class SharedData
    {
        public static ObservableCollection<Customers> CustomerList { get; } = new ObservableCollection<Customers>();
    }
}
