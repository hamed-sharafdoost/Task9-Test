using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task9.Model.DataAccess.Interfaces;
using Task9.Model.Entities;

namespace Task9.Model.DataAccess.Repositories
{
    public class CustomerOrderRepository : ICustomerOrder
    {
        private ConnectionProvider connectionProvider;
        public CustomerOrderRepository(ConnectionProvider provider)
        {
            connectionProvider = provider;
        }
        public async Task AddOrderAsync(CustomerOrders order, CustomerOrdersProducts product)
        {
            var db = connectionProvider.ConnectToDatabase();
            db.Open();
            order.DateOrderPlaced = DateTime.UtcNow;
            order.DateOrderPaid = null;
            order.OrderStatusCode = await db.ExecuteScalarAsync<int>("select OrderStatusCode from dbo.OrderStatusCodes where StatusName = 'Shopping';");
            var dbTran = db.BeginTransaction();
            try
            {
                string customerOrderSql = "Insert Into dbo.CustomerOrders Values " +
                                          "(@CustomerID,@DateOrderPlaced,@OrderPrice,@DateOrderPaid,@PaymentMethodID,@OrderStatusCode);";
                await db.ExecuteAsync(customerOrderSql, order, dbTran);
                string orderIDSql = "Select OrderID from dbo.CustomerOrders where DateOrderPlaced = @Date and CustomerID = @cusId;";
                product.OrderID = await db.ExecuteScalarAsync<int>(orderIDSql, new { Date = order.DateOrderPlaced, cusId = order.CustomerID }, dbTran);
                string customerOrderProduct = "Insert Into dbo.CustomerOrdersProducts Values " +
                                              "(@OrderID,@ProductID,@Quantity,@Comments);";
                await db.ExecuteAsync(customerOrderProduct, product, dbTran);
                dbTran.Commit();
            }
            catch (Exception ex)
            {
                dbTran.Rollback();
            }
            db.Close();
        }
        public async Task<bool> DeleteAsync(int orderId)
        {
            var db = connectionProvider.ConnectToDatabase();
            db.Open();
            string deleteQuery = "Delete from dbo.CustomerOrders where OrderID = @orderID and OrderStatusCode = @Code;";
            string statusQuery = "select OrderStatusCode from dbo.OrderStatusCodes where StatusName = 'Shopping';";
            int orderStatusCode = await db.QuerySingleOrDefaultAsync<int>(statusQuery);
            var dbTran = db.BeginTransaction();
            try
            {
                int rowsAffected = await db.ExecuteAsync(deleteQuery, new {OrderID = orderId,Code = orderStatusCode }, dbTran);
                if (rowsAffected > 0)
                {
                    string deleteFromOrderProduct = "Delete from dbo.CustomerOrdersProducts where OrderID = @OrderId;";
                    var result = await db.ExecuteAsync(deleteFromOrderProduct, new { OrderId = orderId }, dbTran) > 0;
                    dbTran.Commit();
                    return result;
                }
                else
                {
                    dbTran.Rollback();
                    return false;
                }
            }
            catch(Exception ex)
            {
                dbTran.Rollback();
                return false;
            }
        }
        public async Task<IEnumerable<CustomerOrders>> GetAllAsync()
        {
            var db = connectionProvider.ConnectToDatabase();
            var result = await db.GetListAsync<CustomerOrders>();
            db.Close();
            return result;
        }

        public async Task<IEnumerable<CustomerOrders>> GetAllAsync(DateTime start, DateTime end)
        {
            var db = connectionProvider.ConnectToDatabase();
            string dateTimeSql = "select * from dbo.CustomerOrders where DateOrderPlaced > @Start and DateOrderPlaced < @End;";
            var result =  await db.QueryAsync<CustomerOrders>(dateTimeSql, new { Start = start, End = end });
            db.Close();
            return result;
        }

        public async Task<IEnumerable<CustomerOrders>> GetAllAsync(int price, bool upper)
        {
            var db = connectionProvider.ConnectToDatabase();
            if (upper)
            {
                string priceSql = "select * from dbo.CustomerOrders where OrderPrice > @Price;";
                var result = await db.QueryAsync<CustomerOrders>(priceSql, new { Price = price });
                db.Close();
                return result;
            }
            else
            {
                string priceSql = "select * from dbo.CustomerOrders where OrderPrice < @Price;";
                var result = await db.QueryAsync<CustomerOrders>(priceSql, new { Price = price });
                db.Close();
                return result;
            }
        }

        public async Task<IEnumerable<CustomerOrders>> GetAllAsync(int productID)
        {
            var db = connectionProvider.ConnectToDatabase();
            string productOrderSql = "select OrderID from dbo.CustomerOrdersProducts where ProductID = @productId;";
            List<int> orders =(await db.QueryAsync<int>(productOrderSql, new { productId = productID })).AsList();
            List<CustomerOrders> ordersList = new List<CustomerOrders>();
            foreach (var Id in orders)
            {
                ordersList.Add(await db.QuerySingleAsync<CustomerOrders>("select * from dbo.CustomerOrders where OrderID = @orderId", new { orderId = Id }));
            }
            db.Close();
            return ordersList;
        }

        public async Task<IEnumerable<int>> GetOrderIDAsync(int customerId)
        {
            var db = connectionProvider.ConnectToDatabase();
            int orderStatus = await db.ExecuteScalarAsync<int>("select OrderStatusCode from dbo.OrderStatusCodes where StatusName = 'Shopping';");
            string getOrderIdSql = "Select OrderID from dbo.CustomerOrders where CustomerID = @cusId and OrderStatusCode = @status;";
            var result = await db.QueryAsync<int>(getOrderIdSql, new { cusId = customerId, status = orderStatus });
            db.Close();
            return result;
        }

        public async Task<bool> UpdateAsync(CustomerOrdersProducts product, bool add, int productPrice)
        {
            var db = connectionProvider.ConnectToDatabase();
            db.Open();
            string orderStatusSql = "select OrderStatusCode from dbo.OrderStatusCodes where StatusName = 'Shopping';";
            int statusCode = await db.ExecuteScalarAsync<int>(orderStatusSql);
            string statusSql = "select count(OrderID) from dbo.CustomerOrders where OrderID = @orderId and OrderStatusCode = @Code;";
            int verifiedOrder = await db.ExecuteScalarAsync<int>(statusSql, new { orderId = product.OrderID, Code = statusCode });
            if (verifiedOrder > 0)
            {
                string updateOrderPrice = "Update dbo.CustomerOrders set OrderPrice = @price where OrderID = @orderId";
                string orderPriceSql = "Select OrderPrice from dbo.CustomerOrders where OrderID = @OrderId;";
                int orderPrice = await db.ExecuteScalarAsync<int>(orderPriceSql, new { OrderId = product.OrderID });
                var dbTran = db.BeginTransaction();
                try
                {
                    if (add)
                    {
                        string addSql = "Insert Into dbo.CustomerOrdersProducts Values (@OrderID,@ProductID,@Quantity,@Comments);";
                        await db.ExecuteAsync(updateOrderPrice, new { price = orderPrice + productPrice, orderId = product.OrderID }, dbTran);
                        var result = await db.ExecuteAsync(addSql, product, dbTran) > 0;
                        dbTran.Commit();
                        return result;
                    }
                    else
                    {
                        string deleteSql = "Delete from dbo.CustomerOrdersProducts where OrderID = @OrderID and ProductID = @ProductID;";
                        await db.ExecuteAsync(updateOrderPrice, new { price = orderPrice - productPrice, orderId = product.OrderID }, dbTran);
                        var result = await db.ExecuteAsync(deleteSql, product, dbTran) > 0;
                        dbTran.Commit();
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    return false;
                }
            }
            else
                return false;
        }
    }
}
