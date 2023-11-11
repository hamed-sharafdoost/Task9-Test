using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Commands;
using Task9.Model.DataAccess;
using Task9.Model.DataAccess.Repositories;
using Task9.Model.Entities;

namespace Task9.ViewModel.SupplierViewModel
{
    public class AddSupplierViewModel : ValidationViewModelBase
    {
        public DelegateCommand AddCommand { get; }
        private string supplierName;
        private ConnectionProvider connection;
        private SupplierRepository supplierRepository;
        public AddSupplierViewModel()
        {
            connection = new ConnectionProvider();
            supplierRepository = new SupplierRepository(connection);
            AddCommand = new DelegateCommand(AddSupplier,CanExecute);
        }
        public bool CanExecute(object param) => !HasErrors;
        public string SupplierName
        {
            get { return supplierName; }
            set
            {
                supplierName = value;
                if(string.IsNullOrEmpty(supplierName) || supplierName.Count(b => "0123456789".Contains(b)) == supplierName.Length)
                {
                    AddError("Supplier name is needed to be filled with words(and few numbers)",nameof(SupplierName));
                    AddCommand.RaiseCanExecuteChangedEvent();
                }
                else
                {
                    ClearErrors(nameof(SupplierName));
                    AddCommand.RaiseCanExecuteChangedEvent();
                }    

            }
        }
        private void AddSupplier(object param)
        {
            if(!string.IsNullOrEmpty(SupplierName))
            {
                ClearErrors("All");
                AddCommand.RaiseCanExecuteChangedEvent();
                supplierRepository.Add(new Suppliers { SupplierName = SupplierName });
            }
            else
            {
                AddError("Supplier name is empty","All");
                AddCommand.RaiseCanExecuteChangedEvent();
            }
        }
    }
}
