using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Model.Entities;

namespace Task9.ViewModel.CustomerOrderViewModel
{
    public class ProductsOfCustomer
    {
        public CustomerOrders CustomerOrder { get; set; }
        public CustomerOrdersProducts CustomerOrdersProduct { get; set; }
        public string NameOfProduct { get; set; }
    }
}
