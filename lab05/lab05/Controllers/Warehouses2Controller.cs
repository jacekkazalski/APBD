using lab05.Models;
using lab05.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab05.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Warehouses2Controller : ControllerBase
    {
        private readonly IDbService _dbService;

        public Warehouses2Controller(IDbService dbService)
        {
            _dbService = dbService;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterProductProcedure(ProductWarehouseRegister productWarehouseRegister)
        {
            int tmp = await _dbService.RegisterProductProcedure(productWarehouseRegister);
            return Ok(tmp);
        }
    }
}
