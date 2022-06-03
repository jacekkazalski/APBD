using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kol2.DTO
{
    public class EventDTO
    {
        public int IdEvent { get; set; }
        public string Name { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public IEnumerable<OrganiserDTO> MainOrganisers { get; set; }
        public IEnumerable<OrganiserDTO> OtherOrganisers { get; set; }
    }
}
