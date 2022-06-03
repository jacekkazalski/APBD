using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kol2.Entities
{
    public class EventOrganiser
    {
        public int IdEvent { get; set; }
        public int IdOrganiser { get; set; }
        public bool MainOrganiser { get; set; }

        public virtual Organiser IdOrganiserNavigation { get; set; }
        public virtual Event IdEventNavigation { get; set; }
    }
}
