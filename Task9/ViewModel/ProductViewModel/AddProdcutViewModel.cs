using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Commands;
using Task9.Model.DataAccess;
using Task9.Model.DataAccess.Repositories;
using Task9.Model.Entities;

namespace Task9.ViewModel.ProductViewModel
{
    public class AddProdcutViewModel : ValidationViewModelBase
    {
        public DelegateCommand AddCommand { get; }
        private string name;
        private int price;
        private Suppliers selectedSupplier;
        private ProductTypes selectedProductType;
        private ConnectionProvider connection;
        private ProductRepository productRepository;
        public AddProdcutViewModel()
        {
            connection = new ConnectionProvider();
            productRepository = new ProductRepository(connection);
            AddCommand = new DelegateCommand(AddProduct, CanExecute);
        }
        public bool CanExecute(object param) => !HasErrors;
        public Suppliers SelectedSupplier
        {
            get { return selectedSupplier; }
            set
            {
                selectedSupplier = value;
                if (selectedSupplier == null)
                {
                    AddError("Supplier is empty", nameof(SelectedSupplier));
                    AddCommand.RaiseCanExecuteChangedEvent();
                }
                else
                {
                    ClearErrors(nameof(SelectedSupplier));
                    AddCommand.RaiseCanExecuteChangedEvent();
                }
            }
        }
        public ProductTypes SelectedProductType
        {
            get { return selectedProductType; }
            set
            {
                selectedProductType = value;
                if (selectedSupplier == null)
                {
                    AddError("ProdcutType is empty", nameof(SelectedProductType));
                    AddCommand.RaiseCanExecuteChangedEvent();
                }
                else
                {
                    ClearErrors(nameof(SelectedProductType));
                    AddCommand.RaiseCanExecuteChangedEvent();
                }
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                if (string.IsNullOrEmpty(name) || name.Count(b => "0123456789".Contains(b)) == name.Length)
                {
                    AddError("Product name is empty.Fill it with words(and a few numbers)", nameof(Name));
                    AddCommand.RaiseCanExecuteChangedEvent();
                }
                else
                {
                    ClearErrors(nameof(Name));
                    AddCommand.RaiseCanExecuteChangedEvent();
                }
            }
        }
        public int Price
        {
            get { return price; }
            set
            {
                price = value;
                if (price == 0)
                {
                    AddError("Price is empty", nameof(Price));
                    AddCommand.RaiseCanExecuteChangedEvent();
                }
                else
                {
                    ClearErrors(nameof(Price));
                    AddCommand.RaiseCanExecuteChangedEvent();
                }
            }
        }
        private void AddProduct(object param)
        {
            if (SelectedProductType != null && SelectedSupplier != null && !string.IsNullOrEmpty(Name) && Price != 0)
            {
                ClearErrors("All");
                productRepository.AddProduct(new Products
                {
                    SupplierID = SelectedSupplier.SupplierID,
                    Name = Name,
                    Price = Price,
                    ProductTypeCode = selectedProductType.ProductTypeCode
                });
            }
            else
            {
                AddError("All fields have to be filled", "All");
            }
        }
    }
}
