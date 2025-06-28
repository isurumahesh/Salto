using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.IntegrationTests.Configuration
{
    public static class TestDbSeeder
    {
        public static void Seed(CloudWorksDbContext context)
        {
            // Clear existing data
            context.AccessEvents.RemoveRange(context.AccessEvents);
            context.Schedules.RemoveRange(context.Schedules);
            context.Bookings.RemoveRange(context.Bookings);
            context.AccessPoints.RemoveRange(context.AccessPoints);
            context.SiteProfiles.RemoveRange(context.SiteProfiles);
            context.Profiles.RemoveRange(context.Profiles);
            context.Sites.RemoveRange(context.Sites);
            context.SaveChanges();

            // 1. Create Site(s)
            var site = new Site
            {
                Id = Guid.NewGuid(),
                Name = "Test Site"
            };

            // 2. Create Profile(s)
            var profile = new Profile
            {
                Id = Guid.NewGuid(),
                Email = "test@example.com",
                IdentityId = Guid.NewGuid()
            };

            // 3. Create SiteProfile(s)
            var siteProfile = new SiteProfile
            {
                Id = Guid.NewGuid(),
                SiteId = site.Id,
                ProfileId = profile.Id
            };

            // 4. Create AccessPoint(s)
            var accessPoint = new AccessPoint
            {
                Id = Guid.NewGuid(),
                Name = "Main Gate",
                SiteId = site.Id
            };

            // 5. Create Booking(s)
            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                Name = "Morning Booking",
                SiteId = site.Id,
                AccessPoints = new List<AccessPoint> { accessPoint },
                Profiles = new List<SiteProfile> { siteProfile }
            };

            // 6. Create Schedule(s)
            var schedule = new Schedule
            {
                Id = Guid.NewGuid(),
                StartUtc = DateTime.UtcNow.AddHours(-1),
                EndUtc = DateTime.UtcNow.AddHours(1),
                Value = "BEGIN:VCALENDAR...", // Put your iCal string or leave as dummy.
                SiteId = site.Id,
                BookingId = booking.Id
            };

            // 7. Create AccessEvent(s)
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

            // Add all entities in correct order
            context.Sites.Add(site);
            context.Profiles.Add(profile);
            context.SiteProfiles.Add(siteProfile);
            context.AccessPoints.Add(accessPoint);
            context.Bookings.Add(booking);
            context.Schedules.Add(schedule);
            context.AccessEvents.Add(accessEvent);

            context.SaveChanges();
        }
    }

}
