using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9.ViewModel.AddressViewModel
{
    public class AddressCustomerAddress
    {
        public string Username { get; set; }
        public string City { get; set; }
        public int PostCode { get; set; }
        public string CompleteAddress { get; set; }
        public DateTime DateFrom { get; set; }
    }
}
