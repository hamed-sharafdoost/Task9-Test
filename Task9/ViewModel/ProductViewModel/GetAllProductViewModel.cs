using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Commands;
using Task9.Model.DataAccess;
using Task9.Model.DataAccess.Repositories;

namespace Task9.ViewModel.ProductViewModel
{
    public class GetAllProductViewModel
    {
        public ObservableCollection<DetailedProduct> DetailedProducts { get; } = new ObservableCollection<DetailedProduct>();
        public DelegateCommand GetAllCommand { get; }
        private ConnectionProvider connection;
        private ProductRepository productRepository;
        public GetAllProductViewModel()
        {
            connection = new ConnectionProvider();
            productRepository = new ProductRepository(connection);
            GetAllCommand = new DelegateCommand(GetAllAsync);
        }
        private async void GetAllAsync(object param)
        {
            DetailedProducts.Clear();       
            foreach(var item in await productRepository.GetAllAsync())
            {
                DetailedProducts.Add(new DetailedProduct
                {
                    ProductName = item.Name,
                    SupplierName = SharedData.SupplierList.Single(b => b.SupplierID == item.SupplierID).SupplierName,
                    ProductType = SharedData.ProductTypeList.Single(b => b.ProductTypeCode == item.ProductTypeCode).TypeName,
                    Price = item.Price
                });
            }
        }
    }
}
