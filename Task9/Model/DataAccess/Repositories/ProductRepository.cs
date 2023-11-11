using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task9.Model.DataAccess.Interfaces;
using Task9.Model.Entities;

namespace Task9.Model.DataAccess.Repositories
{
    public class ProductRepository : IProduct
    {
        private ConnectionProvider connectionProvider;
        public ProductRepository(ConnectionProvider provider)
        {
            connectionProvider = provider;
        }
        public void AddProduct(Products product)
        {
            var db = connectionProvider.ConnectToDatabase();
            db.Insert(product);
            db.Close();
        }

        public async Task<IEnumerable<Products>> GetAllAsync()
        {
            var db = connectionProvider.ConnectToDatabase();
            var result = await db.GetListAsync<Products>();
            db.Close();
            return result;
        }
    }
}
