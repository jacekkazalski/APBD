using kolokwium_1.Models;
using kolokwium_1.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolokwium_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly IDbService _dbService;
        public TeamsController(IDbService dbService)
        {
            _dbService = dbService;
        }
        [HttpGet("{idChampionship}")]
        public async Task<IActionResult> GetTeamInChampionshipAsync(int idChampionship)
        {
            try
            {
                return Ok(await _dbService.GetTeamInChampioshipAsync(idChampionship));
            }catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddPlayerAsync(PlayerTeam pt)
        {
            try
            {
                await _dbService.AddPlayerToTeam(pt);
                return Ok("dodano");
            }catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
