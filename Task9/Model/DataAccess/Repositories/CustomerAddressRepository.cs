using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Model.Entities;

namespace Task9.Model.DataAccess.Repositories
{
    public class CustomerAddressRepository
    {
        private ConnectionProvider connectionProvider;
        public CustomerAddressRepository(ConnectionProvider provider)
        {
            connectionProvider = provider;
        }
        public CustomerAddresses Get(int addressId,int customerId)
        {
            var db = connectionProvider.ConnectToDatabase();
            string sql = "select DateFrom,DateTo from dbo.CustomerAddresses where AddressID = @AddressId and CustomerID = @CustomerId;";
            db.Open();
            return db.QuerySingle<CustomerAddresses>(sql,new {AddressId = addressId, CustomerId = customerId});
        }
    }
}
