using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9.ViewModel.CustomerOrderDeliveryViewModel
{
    public class CustomizedOrderDelivery
    {
        public string CustomerName { get; set; }
        public DateTime DateOrderPlaced { get; set; }
        public int OrderPrice { get; set; }
        public string DeliveryStatus { get; set; }
    }
}
