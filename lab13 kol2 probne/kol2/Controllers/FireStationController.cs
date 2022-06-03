using kol2.DTO;
using kol2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace kol2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FireStationController : ControllerBase
    {
        private readonly IDbService _dbService;

        public FireStationController(IDbService dbService)
        {
            _dbService = dbService;
        }
        [HttpGet("{idAction}")]
        public async Task<IActionResult> Get(int idAction)
        {
            var response = await _dbService.GetActionAsync(idAction);
            if(response.StatusCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> AddFiretruck(AddFireTruckDTO addFireTruckDTO )
        {
            var response = await _dbService.AddFireTruckToActionAsync(addFireTruckDTO);
            if(response.StatusCode == StatusCodes.Status404NotFound )
            {
                return NotFound(response);
            }
            if (response.StatusCode == StatusCodes.Status400BadRequest)
            {
                return BadRequest(response);
            }
            return Created(response.Message,addFireTruckDTO);

        }
    }
}
