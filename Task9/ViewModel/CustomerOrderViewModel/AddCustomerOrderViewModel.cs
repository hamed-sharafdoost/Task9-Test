using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Commands;
using Task9.Model.DataAccess;
using Task9.Model.DataAccess.Repositories;
using Task9.Model.Entities;

namespace Task9.ViewModel.CustomerOrderViewModel
{
    public class AddCustomerOrderViewModel : ValidationViewModelBase
    {
        public ObservableCollection<ProductsOfCustomer> SelectedProducts { get; } = new ObservableCollection<ProductsOfCustomer>();
        public bool OnSpot { get; set; } = true;
        public int OrderPrice { get; set; }
        public DelegateCommand AddOrder { get; }
        public DelegateCommand AddProduct { get; }
        private string comment;
        private bool orderIsReady = false;
        private int totalPice;
        private int amount;
        private Products selectedProduct;
        private Customers selectedCustomer;
        private ConnectionProvider connection;
        private CustomerOrderRepository customerOrderRepository;
        public AddCustomerOrderViewModel()
        {
            AddProduct = new DelegateCommand(addNewProduct,CanAddProductExecute);
            AddOrder = new DelegateCommand(addNewOrderAsync,CanAddOrderExecute);
            connection = new ConnectionProvider();
            customerOrderRepository = new CustomerOrderRepository(connection);
            AddOrder.RaiseCanExecuteChangedEvent();
        }
        public bool CanAddProductExecute(object param) => !HasErrors;
        public bool CanAddOrderExecute(object param) => orderIsReady;
        public int Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                TotalPrice = 0;
                if(SelectedProduct != null)
                {
                    TotalPrice += amount * SelectedProduct.Price;
                }
            }
        }
        public string Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                if(string.IsNullOrEmpty(comment))
                {
                    AddError("Comment should be filled", nameof(Comment));
                    AddProduct.RaiseCanExecuteChangedEvent();
                }
                else
                {
                    ClearErrors(nameof(Comment));
                    AddProduct.RaiseCanExecuteChangedEvent();
                }
            }
        }
        public int TotalPrice
        {
            get { return totalPice; }
            set
            {
                totalPice = value;
                OnPropertyChanged();
            }
        }
        public Products SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                if(selectedProduct == null)
                {
                    AddError("Select Product", nameof(SelectedProduct));
                    AddProduct.RaiseCanExecuteChangedEvent();
                }
                else
                {
                    ClearErrors(nameof(SelectedProduct));
                    AddProduct.RaiseCanExecuteChangedEvent();
                    TotalPrice += Amount * SelectedProduct.Price;
                }
            }
        }
        public Customers SelectedCustomer
        {
            get { return selectedCustomer; }
            set
            {
                selectedCustomer = value;
                if (selectedCustomer == null)
                {
                    AddError("Select Customer", nameof(SelectedCustomer));
                    AddProduct.RaiseCanExecuteChangedEvent();
                }
                else
                {
                    ClearErrors(nameof(SelectedCustomer));
                    AddProduct.RaiseCanExecuteChangedEvent();
                }
            }
        }
        private void addNewProduct(object param)
        {
            if(SelectedCustomer != null && SelectedProduct != null)
            {
                OrderPrice = TotalPrice;
                TotalPrice = 0;
                SelectedProducts.Add(
                    new ProductsOfCustomer
                    {
                        CustomerOrder = new CustomerOrders { CustomerID = SelectedCustomer.CustomerID,OrderPrice = OrderPrice,PaymentMethodID = OnSpot ? 1: 2 },
                        CustomerOrdersProduct = new CustomerOrdersProducts { ProductID = SelectedProduct.ProductID,Quantity = Amount,Comments = Comment},
                        NameOfProduct = SelectedProduct.Name
                    });
                orderIsReady = true;
                AddOrder.RaiseCanExecuteChangedEvent();
            }
            else
            {
                orderIsReady= false;
                AddOrder.RaiseCanExecuteChangedEvent();
            }
        }
        private async void addNewOrderAsync(object param)
        {
            OrderPrice = 0;
            int orderId = 0;
            for(int i = 0;i < SelectedProducts.Count;i++)
            {
                if(orderId != 0)
                {
                    SelectedProducts[i].CustomerOrdersProduct.OrderID = orderId;
                }
                if (i == 0)
                {
                    await customerOrderRepository.AddOrderAsync(SelectedProducts[i].CustomerOrder, SelectedProducts[i].CustomerOrdersProduct);
                    orderId = (await customerOrderRepository.GetOrderIDAsync(SelectedProducts[i].CustomerOrder.CustomerID)).ToList()[0];
                }
                else
                {
                    await customerOrderRepository.UpdateAsync(SelectedProducts[i].CustomerOrdersProduct, true,
                        SelectedProducts[i].CustomerOrdersProduct.Quantity * 
                        SharedData.ProductList.First(b => b.ProductID == SelectedProducts[i].CustomerOrdersProduct.ProductID).Price);
                }
            }
            SelectedProducts.Clear();
        }
    }
}
