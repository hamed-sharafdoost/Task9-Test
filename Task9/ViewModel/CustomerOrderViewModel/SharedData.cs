using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Model.Entities;
using Task9.ViewModel.CustomerOrderViewModel.GetViewModels;

namespace Task9.ViewModel.CustomerOrderViewModel
{
    public class SharedData
    {
        public static ObservableCollection<Products> ProductList { get; } = new ObservableCollection<Products>();
        public static List<Customers> CustomerList { get; } = new List<Customers>();
        public static ObservableCollection<CustomizedOrder> Orders { get; } = new ObservableCollection<CustomizedOrder>();
    }
}
