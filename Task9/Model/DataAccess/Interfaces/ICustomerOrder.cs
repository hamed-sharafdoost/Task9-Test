using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Model.Entities;

namespace Task9.Model.DataAccess.Interfaces
{
    public interface ICustomerOrder
    {
        Task AddOrderAsync(CustomerOrders order, CustomerOrdersProducts product);
        Task<bool> UpdateAsync(CustomerOrdersProducts product,bool add,int price);
        Task<bool> DeleteAsync(int orderId);
        Task<IEnumerable<CustomerOrders>> GetAllAsync();
        Task<IEnumerable<CustomerOrders>> GetAllAsync(DateTime start, DateTime end);
        Task<IEnumerable<CustomerOrders>> GetAllAsync(int price,bool upper);
        Task<IEnumerable<CustomerOrders>> GetAllAsync(int productId);
        Task<IEnumerable<int>> GetOrderIDAsync(int customerId);
    }
}
