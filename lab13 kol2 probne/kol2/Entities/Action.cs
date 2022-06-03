using System;
using System.Collections.Generic;

namespace kol2.Entities
{
    public class Action
    {
        public int IdAction { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool NeedSpecialEquipment { get; set; }

        public virtual ICollection<FireTruckAction> FireTruckActions { get; set; }

        public Action()
        {
            FireTruckActions = new HashSet<FireTruckAction>();
        }
    }
}
