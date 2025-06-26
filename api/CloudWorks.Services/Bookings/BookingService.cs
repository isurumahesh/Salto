using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Data.Database;
using CloudWorks.Services.Contracts.Bookings;
using Microsoft.EntityFrameworkCore;

namespace CloudWorks.Services.Bookings;

public class BookingService : IBookingService
{
    private readonly CloudWorksDbContext _cloudWorksDbContext;

    public BookingService(CloudWorksDbContext cloudWorksDbContext)
    {
        _cloudWorksDbContext = cloudWorksDbContext;
    }

    public async Task<Booking> AddBooking(
        Guid siteId,
        string name,
        List<string> userEmails,
        List<Guid> accessPoints,
        List<Schedule> schedules,
        CancellationToken cancellationToken
    )
    {
        var accessPointsEntities = await _cloudWorksDbContext.Set<AccessPoint>()
            .Where(x => x.SiteId == siteId && accessPoints!.Contains(x.Id))
            .ToListAsync(cancellationToken);

        var booking = new Booking()
        {
            Id = Guid.NewGuid(),
            Name = name,
            SiteId = siteId,
            Schedules = schedules,
            AccessPoints = accessPointsEntities
        };
        
        await _cloudWorksDbContext.Set<Booking>().AddAsync(
            booking,
            cancellationToken
        );

        await _cloudWorksDbContext.SaveChangesAsync(cancellationToken);

        foreach (var email in userEmails)
        {
            var siteProfile = new SiteProfile()
            {
                Id = Guid.NewGuid(),
                SiteId = siteId,
                Profile = new Profile()
                {
                    Id = Guid.NewGuid(),
                    Email = email
                }
            };

            await _cloudWorksDbContext.Set<SiteProfile>().AddAsync(
                siteProfile,
                cancellationToken
            );

            booking.Profiles.Add(siteProfile);

            await _cloudWorksDbContext.SaveChangesAsync(cancellationToken);
        }

        return booking;
    }
}
