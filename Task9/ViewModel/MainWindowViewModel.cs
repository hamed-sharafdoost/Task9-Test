using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Commands;

namespace Task9.ViewModel
{
    public class MainWindowViewModel
    {
        public DelegateCommand Customer { get; }
        public DelegateCommand Address { get; }
        public DelegateCommand Supplier { get; }
        public DelegateCommand Product { get; }
        public DelegateCommand CustomerOrder { get; }
        public DelegateCommand CustomerOrderDelivery { get; }
        public MainWindowViewModel()
        {
            Customer = new DelegateCommand(customerButton);
            Address = new DelegateCommand(addressButton);
            Supplier = new DelegateCommand(supplierButton);
            Product = new DelegateCommand(productButton);
            CustomerOrder = new DelegateCommand(customerOrderButton);
            CustomerOrderDelivery = new DelegateCommand(customerOrderDeliveryButton);
        }
        private void customerButton(object param)
        {
            CustomerWindow customerWindow = new CustomerWindow();
            customerWindow.Activate();
            customerWindow.Show();
        }
        private void addressButton(object param)
        {
            AddressWindow addressWindow = new AddressWindow();
            addressWindow.Activate();
            addressWindow.Show();
        }
        private void supplierButton(object param)
        {
            SupplierWindow supplierWindow = new SupplierWindow();
            supplierWindow.Activate();
            supplierWindow.Show();
        }
        private void productButton(object param)
        {
            ProductWindow productWindow = new ProductWindow();
            productWindow.Activate();
            productWindow.Show();
        }
        private void customerOrderButton(object param)
        {
            CustomerOrderWindow customerOrderWindow = new CustomerOrderWindow();
            customerOrderWindow.Activate();
            customerOrderWindow.Show();
        }
        private void customerOrderDeliveryButton(object param)
        {
            CustomerOrderDeliveryWindow customerOrderDeliveryWindow = new CustomerOrderDeliveryWindow();
            customerOrderDeliveryWindow.Activate();
            customerOrderDeliveryWindow.Show();
        }
    }
}
