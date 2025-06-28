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
        List<Guid> siteProfiles,
        List<Guid> accessPoints,
        List<Schedule> schedules,
        CancellationToken cancellationToken
    )
    {
        var accessPointsEntities = await _cloudWorksDbContext.Set<AccessPoint>()
            .Where(x => x.SiteId == siteId && accessPoints!.Contains(x.Id))
            .ToListAsync(cancellationToken);

        var siteProfilesEntities = await _cloudWorksDbContext.Set<SiteProfile>()
            .Where(x => x.SiteId == siteId && siteProfiles!.Contains(x.Id))
            .ToListAsync(cancellationToken);

        var booking = new Booking()
        {
            Id = Guid.NewGuid(),
            Name = name,
            SiteId = siteId, 
            Profiles = siteProfilesEntities,
            Schedules = schedules,
            AccessPoints = accessPointsEntities
        };

        await _cloudWorksDbContext.Set<Booking>().AddAsync(
            booking,
            cancellationToken
        );

        await _cloudWorksDbContext.SaveChangesAsync(cancellationToken);
        return booking;
    }
}