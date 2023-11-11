using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Model.Entities;

namespace Task9.Model.DataAccess.Interfaces
{
    public interface IAddress
    {
        Task AddNewAddressAsync(Addresses address,int customerId);
        Task<bool> UpdateAsync(int addressId,int customerId);
        Task<IEnumerable<Addresses>> GetAddressesAsync(int customerId);
    }
}
