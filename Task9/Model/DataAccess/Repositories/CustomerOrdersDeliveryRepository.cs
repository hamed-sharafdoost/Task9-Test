using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task9.Model.DataAccess.Interfaces;
using Task9.Model.Entities;

namespace Task9.Model.DataAccess.Repositories
{
    public class CustomerOrdersDeliveryRepository : ICustomerOrderDelivery
    {
        private ConnectionProvider connectionProvider;
        public CustomerOrdersDeliveryRepository(ConnectionProvider provider)
        {
            connectionProvider = provider;
        }
        public async Task AddAsync(CustomerOrdersDelivery orderDelivery)
        {
            var db = connectionProvider.ConnectToDatabase();
            db.Open();
            string addSql = "Insert Into dbo.CustomerOrdersDelivery Values (@OrderID,@DateReported,@DeliveryStatusCode);";
            await db.ExecuteAsync(addSql,orderDelivery);
            db.Close();
        }
        public async Task<IEnumerable<CustomerOrdersDelivery>> GetAllAsync()
        {
            var db = connectionProvider.ConnectToDatabase();
            var result = await db.GetListAsync<CustomerOrdersDelivery>();
            db.Close();
            return result;
        }
    }
}
