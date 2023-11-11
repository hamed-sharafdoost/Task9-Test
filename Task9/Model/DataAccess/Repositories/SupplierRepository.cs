using DapperExtensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task9.Model.DataAccess.Interfaces;
using Task9.Model.Entities;

namespace Task9.Model.DataAccess.Repositories
{
    public class SupplierRepository : ISupplier
    {
        private ConnectionProvider connectionProvider;
        public SupplierRepository(ConnectionProvider provider)
        {
            connectionProvider = provider;
        }
        public void Add(Suppliers supplier)
        {
            var db = connectionProvider.ConnectToDatabase();
            db.Open();
            db.Insert(supplier);
            db.Close();
        }
        public async Task<IEnumerable<Suppliers>> GetAllAsync()
        {
            var db = connectionProvider.ConnectToDatabase();
            db.Open();
            var result = await db.GetListAsync<Suppliers>();
            db.Close();
            return result;
        }
    }
}
