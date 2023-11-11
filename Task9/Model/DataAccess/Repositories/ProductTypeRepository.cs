using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Model.Entities;

namespace Task9.Model.DataAccess.Repositories
{
    public class ProductTypeRepository
    {
        private ConnectionProvider connectionProvider;
        public ProductTypeRepository(ConnectionProvider provider)
        {
            connectionProvider = provider;
        }
        public async Task<IEnumerable<ProductTypes>> GetAllAsync()
        {
            var db = connectionProvider.ConnectToDatabase();
            db.Open();
            var result = await db.GetListAsync<ProductTypes>();
            db.Close();
            return result;
        }
    }
}
