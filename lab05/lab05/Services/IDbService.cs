using lab05.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab05.Services
{
    public interface IDbService
    {
        Task<int> RegisterProduct(ProductWarehouseRegister productWarehouseRegister);
        Task<int> RegisterProductProcedure(ProductWarehouseRegister productWarehouseRegister);
    }
}
