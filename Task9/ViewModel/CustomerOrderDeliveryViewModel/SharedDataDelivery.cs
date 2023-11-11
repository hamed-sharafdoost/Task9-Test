using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Model.Entities;

namespace Task9.ViewModel.CustomerOrderDeliveryViewModel
{
    public class SharedDataDelivery
    {
        public static ObservableCollection<Customers> CustomerList { get; } = new ObservableCollection<Customers>();
    }
}
