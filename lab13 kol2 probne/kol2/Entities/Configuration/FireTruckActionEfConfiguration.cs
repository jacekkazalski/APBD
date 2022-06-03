using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace kol2.Entities.Configuration
{
    public class FireTruckActionEfConfiguration : IEntityTypeConfiguration<FireTruckAction>
    {
        public void Configure(EntityTypeBuilder<FireTruckAction> builder)
        {
            builder.HasKey(e => new { e.IdFiretruck, e.IdAction }).HasName("FireTruckAction_pk");
            builder.Property(e => e.AssignmentDate).IsRequired();

            builder.HasOne(e => e.IdFireTruckNavigation)
                .WithMany(e => e.FireTruckActions)
                .HasForeignKey(e => e.IdFiretruck)
                .HasConstraintName("FireTruckAction_FireTruck")
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(e => e.IdActionNavigation)
                .WithMany(e => e.FireTruckActions)
                .HasForeignKey(e => e.IdAction)
                .HasConstraintName("FireTruckAction_Action")
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasData(
                new FireTruckAction { IdFiretruck = 1, IdAction = 1, AssignmentDate = System.DateTime.Today },
                new FireTruckAction { IdFiretruck = 2, IdAction = 1, AssignmentDate = System.DateTime.Today },
                new FireTruckAction { IdFiretruck = 3, IdAction = 1, AssignmentDate = System.DateTime.Today },
                new FireTruckAction { IdFiretruck = 1, IdAction = 2, AssignmentDate = System.DateTime.Today.AddDays(-100) },
                new FireTruckAction { IdFiretruck = 1, IdAction = 3, AssignmentDate = System.DateTime.Today.AddDays(-300) },
                new FireTruckAction { IdFiretruck = 2, IdAction = 3, AssignmentDate = System.DateTime.Today.AddDays(-300) },
                new FireTruckAction { IdFiretruck = 3, IdAction = 4, AssignmentDate = System.DateTime.Today.AddDays(-150) },
                new FireTruckAction { IdFiretruck = 4, IdAction = 4, AssignmentDate = System.DateTime.Today.AddDays(-150) }
               );

            builder.ToTable("FireTruck_Action");
        }
    }
}
