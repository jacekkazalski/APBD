using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kol2.Entities
{
    public class Event
    {
        public int IdEvent { get; set; }
        public string Name { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public virtual ICollection<EventOrganiser> EventOrganisers { get; set; }

        public Event()
        {
            EventOrganisers = new HashSet<EventOrganiser>();
        }
    }
}
