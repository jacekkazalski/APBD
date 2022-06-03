using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kol2.Entities.Configurations
{
    public class EventEfConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.IdEvent).HasName("Event_pk");
            builder.Property(e => e.IdEvent).UseIdentityColumn();

            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.DateFrom).IsRequired();

            builder.HasData(
                new Event { IdEvent = 1, DateFrom = System.DateTime.Today, Name = "super festiwal" },
                new Event { IdEvent = 2, DateFrom = System.DateTime.Today.AddDays(-10), DateTo = System.DateTime.Today.AddDays(-5), Name = "skonczony festiwal" },
                new Event { IdEvent = 3, DateFrom = System.DateTime.Today.AddDays(10), DateTo = System.DateTime.Today.AddDays(13), Name = "festiwal z przyszlosci" });

            builder.ToTable("Event");
        }
    }
}
