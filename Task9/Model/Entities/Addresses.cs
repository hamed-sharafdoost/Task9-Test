using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9.Model.Entities
{
    public class Addresses
    {
        public int AddressID { get; set; }
        public string City { get; set; }
        public int PostCode { get; set; }
        public string CompleteAddress { get; set; }
    }
}
