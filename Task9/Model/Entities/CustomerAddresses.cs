using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9.Model.Entities
{
    public class CustomerAddresses
    {
        public int CustomerID { get; set; }
        public int AddressID { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
