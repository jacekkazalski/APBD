using kol2.Entities.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace kol2.Entities
{
    public class FireStationContext : DbContext
    {
        public virtual DbSet<Action> Actions { get; set; }
        public virtual DbSet<FireTruck> FireTrucks { get; set; }
        public virtual DbSet<FireTruckAction> FireTruckActions { get; set; }
        public FireStationContext() 
        { }
        public FireStationContext(DbContextOptions<FireStationContext> options) : base(options) 
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ActionEfConfiguration).GetTypeInfo().Assembly);
        }

    }
}
