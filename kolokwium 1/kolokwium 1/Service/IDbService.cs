using kolokwium_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolokwium_1.Service
{
    public interface IDbService
    {
        public Task<List<Team>> GetTeamInChampioshipAsync(int IdChampionship);
        public Task AddPlayerToTeam(PlayerTeam pt);
    }
}
