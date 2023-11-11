using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Model.Entities;

namespace Task9.ViewModel.CustomerOrderViewModel.GetViewModels
{
    public class CustomizedOrder
    {
        public string CustomerName { get; set; }
        public List<string> Products { get; set; } = new List<string>();
        public List<int> Amount { get; set; } = new List<int>();
        public DateTime DateOrderPlaced { get; set; }
        public int Price { get; set; }
        public string OrderStatus { get; set; }
        public string Paymentmethod { get; set; }
    }
}
