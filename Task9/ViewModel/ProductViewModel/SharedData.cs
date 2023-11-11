using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Model.Entities;

namespace Task9.ViewModel.ProductViewModel
{
    public class SharedData
    {
        public static List<Suppliers> SupplierList { get; } = new List<Suppliers>();
        public static List<ProductTypes> ProductTypeList { get; } = new List<ProductTypes>();
    }
}
