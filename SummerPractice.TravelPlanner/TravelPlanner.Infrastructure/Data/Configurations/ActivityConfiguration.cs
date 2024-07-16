namespace TravelPlanner.Infrastructure.Data.Configurations
{
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.ToTable("Activity");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Duration)
                .IsRequired();

            builder.Property(x => x.Day)
                .IsRequired();

            builder.HasOne(x => x.TravelPlan)
                .WithMany(x => x.Activities)
                .HasForeignKey(x => x.TravelPlanId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new { x.Name, x.TravelPlanId })
                .IsUnique();
        }
    }

}
