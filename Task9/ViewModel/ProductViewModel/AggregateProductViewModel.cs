using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Model.DataAccess;
using Task9.Model.DataAccess.Repositories;

namespace Task9.ViewModel.ProductViewModel
{
    public class AggregateProductViewModel
    {
        public AddProdcutViewModel AddProdcutViewModel { get; }
        public GetAllProductViewModel GetAllProductViewModel { get; }
        public SharedData SharedData { get; }
        private ConnectionProvider connection;
        private SupplierRepository supplierRepository;
        private ProductTypeRepository typeRepository;
        public AggregateProductViewModel()
        {
            SharedData.ProductTypeList.Clear();
            SharedData.SupplierList.Clear();
            connection = new ConnectionProvider();
            SharedData = new SharedData();
            AddProdcutViewModel = new AddProdcutViewModel();
            GetAllProductViewModel = new GetAllProductViewModel();
            supplierRepository = new SupplierRepository(connection);
            typeRepository = new ProductTypeRepository(connection);
            loadSuppliersAsync();
            loadTypesAsync();
        }
        private async void loadSuppliersAsync()
        {
            foreach (var item in await supplierRepository.GetAllAsync())
            {
                SharedData.SupplierList.Add(item);
            }
        }
        private async void loadTypesAsync()
        {
            foreach (var item in await typeRepository.GetAllAsync())
            {
                SharedData.ProductTypeList.Add(item);
            }
        }
    }
}
