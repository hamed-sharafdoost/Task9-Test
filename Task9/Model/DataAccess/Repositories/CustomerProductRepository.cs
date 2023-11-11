using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Model.Entities;

namespace Task9.Model.DataAccess.Repositories
{
    public class CustomerProductRepository
    {
        private ConnectionProvider connectionProvider;
        public CustomerProductRepository(ConnectionProvider provider)
        {
            connectionProvider = provider;
        }
        public IEnumerable<CustomerOrdersProducts> GetAll()
        {
            var db = connectionProvider.ConnectToDatabase();
            return db.GetList<CustomerOrdersProducts>();
        }
        public IEnumerable<int> GetProductId(int orderId)
        {
            var db = connectionProvider.ConnectToDatabase();
            string getProductIdSql = "Select ProductID from dbo.CustomerOrdersProducts where OrderID = @orderId;";
            return db.Query<int>(getProductIdSql,new {orderId = orderId});
        }
    }
}
