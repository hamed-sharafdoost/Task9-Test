using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Model.Entities;

namespace Task9.Model.DataAccess.Interfaces
{
    public interface ISupplier
    {
        void Add(Suppliers supplier);
        Task<IEnumerable<Suppliers>> GetAllAsync();
    }
}
