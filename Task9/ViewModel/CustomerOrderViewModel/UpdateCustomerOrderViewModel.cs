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
    public class UpdateCustomerOrderViewModel : ValidationViewModelBase
    {
        public ObservableCollection<Products> ProductsForRemove { get; } = new ObservableCollection<Products>();
        public ObservableCollection<int> Orders { get; } = new ObservableCollection<int>();
        public DelegateCommand AddCommand { get; }
        public DelegateCommand RemoveCommand { get; }
        public string Comment { get; set; }
        private Products selectAddProduct;
        private Products selectRemoveProduct;
        private int amount;
        private int totalPriceAdd;
        private int totalPriceRemove;
        private bool changingIsEnabled;
        private bool selectOrderIsEnabled;
        private Customers selectedUser;
        private int selectedOrder;
        private ConnectionProvider connection;
        private CustomerOrderRepository orderRepository;
        private CustomerProductRepository productRepository;
        public UpdateCustomerOrderViewModel()
        {
            connection = new ConnectionProvider();
            orderRepository = new CustomerOrderRepository(connection);
            productRepository = new CustomerProductRepository(connection);
            AddCommand = new DelegateCommand(addProductAsync);
            RemoveCommand = new DelegateCommand(removeProductAsync);
        }
        public Products SelectAddProduct
        {
            get { return selectAddProduct; }
            set
            {
                selectAddProduct = value;
                TotalPriceAdd = Amount * selectAddProduct.Price;
            }
        }
        public Products SelectRemoveProduct
        {
            get { return selectRemoveProduct; }
            set
            {
                selectRemoveProduct = value;
                TotalPriceRemove = selectRemoveProduct.Price;
            }
        }
        public bool SelectOrderIsEnabled
        {
            get { return selectOrderIsEnabled; }
            set
            {
                selectOrderIsEnabled = value;
                OnPropertyChanged();
            }
        }
        public bool ChangingIsEnabled
        {
            get { return changingIsEnabled; }
            set
            {
                changingIsEnabled = value;
                OnPropertyChanged();
            }
        }
        public Customers SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                if(selectedUser != null)
                {
                    getOrdersAsync(SelectedUser.CustomerID);
                    if (Orders.Any())
                    {
                        SelectOrderIsEnabled = true;
                    }
                    else
                    {
                        SelectOrderIsEnabled = false;
                    }
                }
            }
        }
        public int Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                if(SelectAddProduct != null)
                {
                    TotalPriceAdd = amount * SelectAddProduct.Price;
                }
            }
        }
        public int TotalPriceAdd
        {
            get { return totalPriceAdd; }
            set
            {
                totalPriceAdd = value;
                OnPropertyChanged();
            }
        }
        public int TotalPriceRemove
        {
            get { return totalPriceRemove; }
            set
            {
                totalPriceRemove = value;
                OnPropertyChanged();
            }
        }
        public int SelectedOrder
        {
            get { return selectedOrder; }
            set
            {
                selectedOrder = value;
                if(selectedOrder != 0)
                {
                    getProducts(SelectedOrder);
                    if (ProductsForRemove.Any())
                    {
                        ChangingIsEnabled = true;
                    }
                    else
                    {
                        ChangingIsEnabled = false;
                    }
                }
            }
        }
        private async void getOrdersAsync(int customerId)
        {
            Orders.Clear();
            foreach (var item in await orderRepository.GetOrderIDAsync(customerId))
            {
                Orders.Add(item);
            }
        }
        private void getProducts(int orderId)
        {
            ProductsForRemove.Clear();
            foreach(var item in productRepository.GetProductId(orderId))
            {
                ProductsForRemove.Add(SharedData.ProductList.First(b => b.ProductID == item));
            }
        }
        private async void addProductAsync(object param)
        {
            if(SelectAddProduct != null && Amount != 0 && !string.IsNullOrEmpty(Comment) && SelectedOrder != 0)
            {
                await orderRepository.UpdateAsync(new CustomerOrdersProducts
                {
                    OrderID = SelectedOrder,
                    ProductID = SelectAddProduct.ProductID,
                    Comments = Comment,
                    Quantity = Amount
                },true,Amount * SelectAddProduct.Price);
            }
        }
        private async void removeProductAsync(object param)
        {
            if(SelectRemoveProduct != null)
            {
                await orderRepository.UpdateAsync(new CustomerOrdersProducts
                {
                    OrderID = SelectedOrder,
                    ProductID = SelectRemoveProduct.ProductID
                }
                , false,SelectRemoveProduct.Price);
                ProductsForRemove.Remove(SelectRemoveProduct);
            }
        }
    }
}
