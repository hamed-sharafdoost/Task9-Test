using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Model.Entities;

namespace Task9.ViewModel.CustomerViewModel
{
    public class SharedData
    {
        public static ObservableCollection<Customers> customers { get; } = new ObservableCollection<Customers>();
    }
}
