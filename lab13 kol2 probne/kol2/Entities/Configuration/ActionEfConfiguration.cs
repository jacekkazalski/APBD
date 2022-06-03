using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace kol2.Entities.Configuration
{
    public class ActionEfConfiguration : IEntityTypeConfiguration<Action>
    {
        public void Configure(EntityTypeBuilder<Action> builder)
        {
            builder.HasKey(e => e.IdAction).HasName("Action_pk");
            builder.Property(e => e.IdAction).UseIdentityColumn();

            builder.Property(e => e.StartTime).IsRequired();
            builder.Property(e => e.NeedSpecialEquipment).IsRequired();

            builder.HasData(
                new Action { IdAction = 1, StartTime = System.DateTime.Today, EndTime = System.DateTime.Today.AddDays(3), NeedSpecialEquipment = true },
                new Action { IdAction = 2, StartTime = System.DateTime.Today.AddDays(-100), EndTime = System.DateTime.Today.AddDays(-50), NeedSpecialEquipment = false },
                new Action { IdAction = 3, StartTime = System.DateTime.Today.AddDays(-300), EndTime = System.DateTime.Today.AddDays(-299), NeedSpecialEquipment = false },
                new Action { IdAction = 4, StartTime = System.DateTime.Today.AddDays(-150), NeedSpecialEquipment = true }
            );


            builder.ToTable("Action");
        }
    }
}
