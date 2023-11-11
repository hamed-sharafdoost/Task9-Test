using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Task9.Commands;
using Task9.Model.DataAccess;
using Task9.Model.DataAccess.Repositories;
using Task9.Model.Entities;

namespace Task9.ViewModel.SupplierViewModel
{
    public class GetAllSupplierViewModel
    {
        public DelegateCommand GetCommand { get; }
        public ObservableCollection<Suppliers> SupplierList { get; } = new ObservableCollection<Suppliers>();
        private ConnectionProvider connection;
        private SupplierRepository supplierRepository;
        public GetAllSupplierViewModel()
        {
            connection = new ConnectionProvider();
            supplierRepository = new SupplierRepository(connection);
            GetCommand = new DelegateCommand(GetAllAsync);
        }
        private async void GetAllAsync(object param)
        {
            SupplierList.Clear();
            foreach(var item in await supplierRepository.GetAllAsync())
            {
                SupplierList.Add(item);
            }
        }
    }
}
