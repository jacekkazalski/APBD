using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using probne_kolokwium01.Models;
using probne_kolokwium01.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace probne_kolokwium01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IDbService _dbService;
        public PrescriptionController(IDbService dbService)
        {
            _dbService = dbService;
        }
        [HttpGet("{name}")]
        public async Task<IActionResult> GetPrescriptions(string name)
        {
            List<Prescription> result = await _dbService.GetPerscriptions(name);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPrescriptions()
        {
            List<Prescription> result = await _dbService.GetAllPerscriptions();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddMedicamentToPrescription(List<Prescription_Medicament> list)
        {
            try
            {
                await _dbService.AddMedicamentToPrescription(list);
                return Ok("dodano");
            }catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
