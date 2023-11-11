using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9.Model.Entities
{
    public class Products
    {
        public int ProductID { get; set; }
        public int SupplierID { get; set; }
        public int ProductTypeCode { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }
}
