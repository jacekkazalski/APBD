using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kol2.Entities.Configurations
{
    public class EventOrganiserEfConfiguration : IEntityTypeConfiguration<EventOrganiser>
    {
        public void Configure(EntityTypeBuilder<EventOrganiser> builder)
        {
            builder.HasKey(e => new { e.IdEvent, e.IdOrganiser }).HasName("EventOrganiser_pk");
            builder.Property(e => e.MainOrganiser).IsRequired();

            builder.HasOne(e => e.IdEventNavigation)
                .WithMany(e => e.EventOrganisers)
                .HasForeignKey(e => e.IdEvent)
                .HasConstraintName("EventOrganiser_Event")
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(e => e.IdOrganiserNavigation)
                .WithMany(e => e.EventOrganisers)
                .HasForeignKey(e => e.IdOrganiser)
                .HasConstraintName("EventOrganiser_Organiser")
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasData(
                new EventOrganiser { IdEvent = 1, IdOrganiser = 1, MainOrganiser = true },
                new EventOrganiser { IdEvent = 2, IdOrganiser = 2, MainOrganiser = true },
                new EventOrganiser { IdEvent = 2, IdOrganiser = 3, MainOrganiser = false },
                new EventOrganiser { IdEvent = 3, IdOrganiser = 2, MainOrganiser = true });

            builder.ToTable("Event_Organiser");
        }
    }
}
