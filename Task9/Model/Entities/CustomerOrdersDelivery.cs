using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9.Model.Entities
{
    public class CustomerOrdersDelivery
    {
        public int OrderID { get; set; }
        public DateTime DateReported { get; set; }
        public int DeliveryStatusCode { get; set; }
    }
}
