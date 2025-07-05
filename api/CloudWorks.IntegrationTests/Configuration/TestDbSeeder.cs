using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Data.Database;

namespace CloudWorks.IntegrationTests.Configuration
{
    public static class TestDbSeeder
    {
        public static async Task Seed(CloudWorksDbContext context)
        {
            context.AccessEvents.RemoveRange(context.AccessEvents);
            context.Schedules.RemoveRange(context.Schedules);
            context.Bookings.RemoveRange(context.Bookings);
            context.AccessPoints.RemoveRange(context.AccessPoints);
            context.SiteProfiles.RemoveRange(context.SiteProfiles);
            context.Profiles.RemoveRange(context.Profiles);
            context.Sites.RemoveRange(context.Sites);
            context.SaveChanges();

            var site = new Site
            {
                Id = Guid.NewGuid(),
                Name = "Test Site"
            };

            var profile = new Profile
            {
                Id = Guid.NewGuid(),
                Email = "test@example.com",
                IdentityId = Guid.NewGuid()
            };

            var siteProfile = new SiteProfile
            {
                Id = Guid.NewGuid(),
                SiteId = site.Id,
                ProfileId = profile.Id
            };

            var accessPoint = new AccessPoint
            {
                Id = Guid.NewGuid(),
                Name = "Main Gate",
                SiteId = site.Id
            };

            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                Name = "Morning Booking",
                SiteId = site.Id,
                AccessPoints = new List<AccessPoint> { accessPoint },
                Profiles = new List<SiteProfile> { siteProfile }
            };

            var schedule = new Schedule
            {
                Id = Guid.NewGuid(),
                StartUtc = DateTime.UtcNow.AddHours(-1),
                EndUtc = DateTime.UtcNow.AddHours(1),
                Value = "BEGIN:VCALENDAR...",
                SiteId = site.Id,
                BookingId = booking.Id
            };

            var accessEvent = new AccessEvent
            {
                Id = Guid.NewGuid(),
                SiteId = site.Id,
                AccessPointId = accessPoint.Id,
                ProfileId = profile.Id,
                Success = true,
                Timestamp = DateTime.UtcNow,
                Reason = "Test entry"
            };

            await context.Sites.AddAsync(site);
            await context.Profiles.AddAsync(profile);
            await context.SiteProfiles.AddAsync(siteProfile);
            await context.AccessPoints.AddAsync(accessPoint);
            await context.Bookings.AddAsync(booking);
            await context.Schedules.AddAsync(schedule);
            await context.AccessEvents.AddAsync(accessEvent);
            await context.SaveChangesAsync();

            TestData.Set(site.Id, profile.Id, accessPoint.Id, booking.Id, siteProfile.Id);
        }

        public static void SeedBulkSites(CloudWorksDbContext context, int count)
        {
            var sites = Enumerable.Range(1, count).Select(i => new Site
            {
                Id = Guid.NewGuid(),
                Name = $"Site {i}"
            }).ToList();

            context.Sites.AddRange(sites);
            context.SaveChanges();
        }
    }
}