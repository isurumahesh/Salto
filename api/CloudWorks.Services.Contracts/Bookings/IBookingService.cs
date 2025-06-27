using CloudWorks.Data.Contracts.Entities;

namespace CloudWorks.Services.Contracts.Bookings;

public interface IBookingService
{
    Task<Booking> AddBooking(
        Guid siteId,
        string name,
        List<string> userEmails,
        List<Guid> accessPoints,
        List<Schedule> schedules,
        CancellationToken cancellationToken
    );
}