using kol2.DTO;
using kol2.Entities;
using kol2.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kol2.Service
{
    public class DbService : IDbService
    {

        private readonly EventOrganiserContext _context;

        public DbService(EventOrganiserContext eventOrganiserContext)
        {
            _context = eventOrganiserContext;
        }
        public async Task<Response<List<EventDTO>>> GetEventsAsync(bool containsEndDate)
        {
            var response = new Response<List<EventDTO>>();
            var listDTO = new List<EventDTO>();
            response.result = listDTO;
            var events = await _context.Events.ToListAsync();
            if (containsEndDate)
            {
                events = await _context.Events.Where(e => e.DateTo != DateTime.MinValue).ToListAsync();
            }
            if(events == null)
            {
                response.StatusCode = StatusCodes.Status404NotFound;
                response.ErrorMessage = "No events found";
                return response;
            }
            foreach(var v in events)
            {
                var mainOrgId = await _context.EventOrganisers.Where(e => e.IdEvent == v.IdEvent && e.MainOrganiser).Select(e => e.IdOrganiser).ToListAsync();
                var otherOrgId = await _context.EventOrganisers.Where(e => e.IdEvent == v.IdEvent && !e.MainOrganiser).Select(e => e.IdOrganiser).ToListAsync();

                var mainOrg = await _context.Organisers.Where(e => mainOrgId.Contains(e.IdOrganiser)).ToListAsync();
                var otherOrg = await _context.Organisers.Where(e => otherOrgId.Contains(e.IdOrganiser)).ToListAsync();

                var mainOrgList = new List<OrganiserDTO>();
                var otherOrgList = new List<OrganiserDTO>();

                foreach(var o in mainOrg)
                {
                    mainOrgList.Add(new OrganiserDTO
                    {
                        IdOrganiser = o.IdOrganiser,
                        Name = o.Name
                    });
                }
                foreach (var o in otherOrg)
                {
                    otherOrgList.Add(new OrganiserDTO
                    {
                        IdOrganiser = o.IdOrganiser,
                        Name = o.Name
                    });
                }

                listDTO.Add(new EventDTO
                {
                    IdEvent = v.IdEvent,
                    Name = v.Name,
                    DateFrom = v.DateFrom,
                    DateTo = v.DateTo,
                    MainOrganisers = mainOrgList,
                    OtherOrganisers = otherOrgList


                });
            }
            response.result = listDTO;
            response.StatusCode = StatusCodes.Status200OK;
            return response;

        }

        public async Task<Response<object>> RemoveEventAsync(int idEvent)
        {
            var response = new Response<object>();
            var ev = await _context.Events.Where(e => e.IdEvent == idEvent).SingleOrDefaultAsync();
            if(ev == null)
            {
                response.StatusCode = StatusCodes.Status404NotFound;
                response.ErrorMessage = "Event not found";
                return response;
            }  
            if(ev.DateFrom < System.DateTime.Today || ev.DateTo != DateTime.MinValue)
            {
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.ErrorMessage = "Can't delete this event";
                return response;
            }
            var evMainOrgs = await _context.EventOrganisers.Where(e => e.IdEvent == idEvent && e.MainOrganiser).ToListAsync();
            if(evMainOrgs.Count >= 3)
            {
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.ErrorMessage = "This event has 3 or more main organisers";
                return response;
            }

            //await _context.EventOrganisers.Remove(await _context.EventOrganisers.Where(e => e.IdEvent == idEvent).SingleAsync()).ReloadAsync();
            //await _context.SaveChangesAsync();

            var eventorganisers = await _context.EventOrganisers.Where(e => e.IdEvent == idEvent).ToListAsync();
            _context.EventOrganisers.RemoveRange(eventorganisers);
            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();

            response.StatusCode = StatusCodes.Status200OK;
            response.ErrorMessage = "removed";
            return response;
        }
    }
}
