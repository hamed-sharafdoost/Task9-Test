﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task9.Model.Entities;

namespace Task9.Model.DataAccess.Interfaces
{
    public interface IProduct
    {
        void AddProduct(Products product);
        Task<IEnumerable<Products>> GetAllAsync();
    }
}
