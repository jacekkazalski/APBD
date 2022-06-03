using kol2.DTO;
using kol2.Response;
using System.Threading.Tasks;

namespace kol2.Services
{
    public interface IDbService
    {
         Task<Response<ActionDTO>> GetActionAsync(int idAction);
        Task<Response<object>> AddFireTruckToActionAsync(AddFireTruckDTO addFireTruckDTO);
    }
}
