using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace kol2.Entities.Configuration
{
    public class FireTruckEfConfiguration : IEntityTypeConfiguration<FireTruck>
    {
        public void Configure(EntityTypeBuilder<FireTruck> builder)
        {
            builder.HasKey(e => e.IdFiretruck).HasName("FireTruck_pk");
            builder.Property(e => e.IdFiretruck).UseIdentityColumn();

            builder.Property(e=> e.OperationNumber).IsRequired().HasMaxLength(10);
            builder.Property(e => e.SpecialEquipment).IsRequired();

            builder.HasData(
                new FireTruck { IdFiretruck = 1, OperationNumber = "1234", SpecialEquipment = false },
                new FireTruck { IdFiretruck = 2, OperationNumber = "420", SpecialEquipment = true },
                new FireTruck { IdFiretruck = 3, OperationNumber = "9999", SpecialEquipment = false },
                new FireTruck { IdFiretruck = 4, OperationNumber = "12455136", SpecialEquipment = true }
                );

            builder.ToTable("FireTruck");
           
        }
    }
}
