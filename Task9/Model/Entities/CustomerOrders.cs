using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9.Model.Entities
{
    public class CustomerOrders
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public DateTime DateOrderPlaced { get; set; }
        public int OrderPrice { get; set; }
        public DateTime? DateOrderPaid { get; set; }
        public int PaymentMethodID { get; set; }
        public int OrderStatusCode { get; set; }
    }
}
