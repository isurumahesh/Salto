using CloudWorks.Data.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace CloudWorks.Data.Database;

public class CloudWorksDbContext : DbContext
{
    public CloudWorksDbContext(DbContextOptions<CloudWorksDbContext> options)
    : base(options)
    {
    }

    public DbSet<Site> Sites { get; set; }
    public DbSet<AccessPoint> AccessPoints { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<SiteProfile> SiteProfiles { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<AccessEvent> AccessEvents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CloudWorksDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}