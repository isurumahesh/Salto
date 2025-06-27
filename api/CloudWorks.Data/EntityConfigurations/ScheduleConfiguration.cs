using CloudWorks.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudWorks.Data.EntityConfigurations;

public sealed class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        builder.ToTable("schedules");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("id");

        builder.Property(x => x.Value).HasColumnName("value");

        builder.Property(x => x.SiteId).HasColumnName("site_id");

        builder.HasOne(x => x.Site).WithMany().HasForeignKey(x => x.SiteId);

        builder.Property(x => x.BookingId).HasColumnName("booking_id");

        builder.HasOne(x => x.Booking).WithMany(x => x.Schedules).HasForeignKey(x => x.BookingId);
    }
}