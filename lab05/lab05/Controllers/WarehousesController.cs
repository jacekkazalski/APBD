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
    public class WarehousesController : ControllerBase
    {
        private readonly IDbService _dbService;

        public WarehousesController(IDbService dbService)
        {
            _dbService = dbService;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterProduct(ProductWarehouseRegister productWarehouseRegister)
        {
            int tmp = await _dbService.RegisterProduct(productWarehouseRegister);
            switch (tmp)
            {
                default:
                    return Ok(tmp);
                    
                case -1:
                    return NotFound("produkt o podanym id nie istnieje");
                    
                case -2:
                    return NotFound("zamowienie o takch parametrach nie istnieje");
                    
                case -3:
                    return NotFound("zamowienie zostalo juz zrealizowane");
                case -4:
                    return NotFound("magazyn o podanym Id nie istnieje");
                    
            }
            
        }
    }
}
