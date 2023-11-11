using Dapper;
using DapperExtensions;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Task9.Model.DataAccess.Interfaces;
using Task9.Model.Entities;

namespace Task9.Model.DataAccess.Repositories
{
    public class AddressRepository : IAddress
    {
        private ConnectionProvider connectionProvider;
        public AddressRepository(ConnectionProvider provider)
        {
            connectionProvider = provider;
        }

        public async Task AddNewAddressAsync(Addresses address, int customerId)
        {
            var db = connectionProvider.ConnectToDatabase();
            {
                db.Open();
                using (var dbTran = db.BeginTransaction())
                {
                    string addressSql = "Insert Into dbo.Addresses Values (@City,@PostCode,@CompleteAddress);";
                    string addressIdSql = "Select AddressID from dbo.Addresses where City = @City and PostCode = @PostCode and CompleteAddress = @CompleteAddress;";
                    string customerAddressSql = "Insert Into dbo.CustomerAddresses Values (@CustomerID,@AddressID,@DateFrom,null);";
                    await db.ExecuteAsync(addressSql, address, dbTran);
                    int addressId = await db.ExecuteScalarAsync<int>(addressIdSql,address,dbTran);
                    await db.ExecuteAsync(customerAddressSql, new
                    {
                        CustomerID = customerId
                                                       ,
                        AddressID = addressId
                                                       ,
                        DateFrom = DateTime.UtcNow
                    }, dbTran);
                    dbTran.Commit();
                }
                db.Close();
            }
        }
        public async Task<IEnumerable<Addresses>> GetAddressesAsync(int customerId)
        {
            var db = connectionProvider.ConnectToDatabase();
            {
                string customerAddressSql = "select AddressID from dbo.CustomerAddresses where CustomerID = @customerId and DateTo is null;";
                string addressesSql = "select * from dbo.Addresses where AddressID = @ID;";
                IEnumerable<int> addressId = await db.QueryAsync<int>(customerAddressSql, new { customerId = customerId });
                List<Addresses> addresses = new List<Addresses>();
                foreach(var Id in addressId)
                {
                    addresses.Add(await db.QuerySingleAsync<Addresses>(addressesSql,new { ID = Id }));
                }
                return addresses;
            }
        }
        public async Task<bool> UpdateAsync(int addressId,int customerId)
        {
            var db = connectionProvider.ConnectToDatabase();
            {
                db.Open();
                string sql = "select DateTo from dbo.CustomerAddresses where AddressID = @addressId and CustomerID = @customerId;";
                string updateSql = "Update dbo.CustomerAddresses set DateTo = @Date where CustomerID=@cusId and AddressID = @AdId;";
                CustomerAddresses address = await db.QuerySingleAsync<CustomerAddresses>(sql, new { addressId = addressId ,customerId = customerId });
                address.DateTo = DateTime.UtcNow;
                return await db.ExecuteAsync(updateSql,new {Date = address.DateTo,cusId = customerId,AdId = addressId}) > 0;
            }
        }
    }
}
