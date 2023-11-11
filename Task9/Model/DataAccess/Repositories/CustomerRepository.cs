using DapperExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task9.Model.DataAccess.Interfaces;
using Task9.Model.Entities;

namespace Task9.Model.DataAccess.Repositories
{
    public class CustomerRepository : ICustomer
    {
        private IConnectionProvider connectionProvider;
        public CustomerRepository(IConnectionProvider connProvider)
        {
            connectionProvider = connProvider;
        }
        public void AddCustomer(Customers customer)
        {
            var db = connectionProvider.ConnectToDatabase();
            db.Open();
            db.Insert(customer);
            db.Close();
        }
        public async Task<IEnumerable<Customers>> GetAll()
        {
            var db = connectionProvider.ConnectToDatabase();
            db.Open();
            var result = await db.GetListAsync<Customers>();
            db.Close();
            return result;
        }
        public void UpdateCustomer(Customers customer)
        {
            var db = connectionProvider.ConnectToDatabase();
            db.Open();
            db.Update(customer);
            db.Close();
        }
    }
}
