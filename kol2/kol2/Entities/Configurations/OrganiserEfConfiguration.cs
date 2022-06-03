using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace kol2.Entities.Configurations
{
    public class OrganiserEfConfiguration : IEntityTypeConfiguration<Organiser>
    {
        public void Configure(EntityTypeBuilder<Organiser> builder)
        {
            builder.HasKey(e => e.IdOrganiser).HasName("Organiser_pk");
            builder.Property(e => e.IdOrganiser).UseIdentityColumn();

            builder.Property(e => e.Name).IsRequired();

            builder.HasData(
                new Organiser { IdOrganiser = 1, Name = "duzy organizator" },
                new Organiser { IdOrganiser = 2, Name = "sredni organizator" },
                new Organiser { IdOrganiser = 3, Name = "maly organizator" });

            builder.ToTable("Organiser");
        }
    }
}
