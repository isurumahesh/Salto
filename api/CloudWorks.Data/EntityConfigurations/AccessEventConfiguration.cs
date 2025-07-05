using CloudWorks.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudWorks.Data.EntityConfigurations
{
    public class AccessEventConfiguration : IEntityTypeConfiguration<AccessEvent>
    {
        public void Configure(EntityTypeBuilder<AccessEvent> builder)
        {
            builder.ToTable("access_events");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.SiteId).HasColumnName("site_id").IsRequired();
            builder.Property(x => x.AccessPointId).HasColumnName("access_point_id").IsRequired();
            builder.Property(x => x.ProfileId).HasColumnName("profile_id").IsRequired();
            builder.Property(x => x.Success).HasColumnName("success");
            builder.Property(x => x.Timestamp).HasColumnName("timestamp");
            builder.Property(x => x.Reason).HasColumnName("reason");

            builder.HasOne(x => x.Site)
                   .WithMany()
                   .HasForeignKey(x => x.SiteId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.AccessPoint)
                   .WithMany()
                   .HasForeignKey(x => x.AccessPointId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Profile)
                   .WithMany()
                   .HasForeignKey(x => x.ProfileId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}