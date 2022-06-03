using System;

namespace kol2.Entities
{
    public class FireTruckAction
    {
        public int IdFiretruck { get; set; }
        public int IdAction { get; set; }
        public DateTime AssignmentDate { get; set; }

        public virtual FireTruck IdFireTruckNavigation { get; set; }
        public virtual Action IdActionNavigation { get; set; }

    }
}
