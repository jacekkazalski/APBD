using kol2.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kol2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventOrganiserController : ControllerBase
    {
        private readonly IDbService _dbService;

        public EventOrganiserController(IDbService dbService)
        {
            _dbService = dbService;
        }
        [HttpGet]
        public async Task<IActionResult> GetEvents(bool containsEndDate)
        {
            var response = await _dbService.GetEventsAsync(containsEndDate);
            if(response.StatusCode == StatusCodes.Status404NotFound)
            {
                return NotFound();
            }
            return Ok(response);
        }

        /*[HttpDelete] nie dziala
        public async Task<IActionResult> RemoveEvent(int idEvent)
        {
            var response = await _dbService.RemoveEventAsync(idEvent);

            return Ok(response);
        }*/
    }
}
