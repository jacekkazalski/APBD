using probne_kolokwium01.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace probne_kolokwium01.Service
{
    public interface IDbService
    {
        Task<List<Prescription>> GetPerscriptions(string name);
        Task<List<Prescription>> GetAllPerscriptions();
        Task AddMedicamentToPrescription(List<Prescription_Medicament> list);
    }
}
