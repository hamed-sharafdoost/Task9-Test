using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task9.ViewModel.SupplierViewModel
{
    public class AggregateSupplierViewModel
    {
        public AddSupplierViewModel AddSupplierViewModel { get; }
        public GetAllSupplierViewModel GetAllSupplierViewModel { get; }
        public AggregateSupplierViewModel()
        {
            AddSupplierViewModel = new AddSupplierViewModel();
            GetAllSupplierViewModel = new GetAllSupplierViewModel();
        }
    }
}
