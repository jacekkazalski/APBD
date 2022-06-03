using kol2.DTO;
using kol2.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kol2.Service
{
    public interface IDbService
    {
        Task<Response<List<EventDTO>>> GetEventsAsync(bool containsEndDate);
        Task<Response<object>> RemoveEventAsync(int idEvent);
    }
}
