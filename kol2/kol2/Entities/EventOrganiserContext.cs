using kol2.Entities.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace kol2.Entities
{
    public class EventOrganiserContext : DbContext
    {
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Organiser> Organisers { get; set; }
        public virtual DbSet<EventOrganiser> EventOrganisers { get; set; }

        public EventOrganiserContext()
        { }
        public EventOrganiserContext(DbContextOptions<EventOrganiserContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(EventEfConfiguration).GetTypeInfo().Assembly);
        }
    }
}
