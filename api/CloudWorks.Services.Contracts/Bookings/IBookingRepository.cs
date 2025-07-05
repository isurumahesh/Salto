using CloudWorks.Data.Contracts.Entities;

namespace CloudWorks.Services.Contracts.Bookings
{
    public interface IBookingRepository
    {
        Task<Booking> AddAsync(Guid siteId, string name, List<Guid> siteProfiles, List<Guid> accessPoints, List<Schedule> schedules, CancellationToken cancellationToken);

        Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        IQueryable<Booking> Query();

        Task<List<Booking>> GetBySiteIdAsync(Guid siteId, CancellationToken cancellationToken);

        Task DeleteAsync(Booking booking, CancellationToken cancellationToken);

        Task<bool> HasValidBookingAsync(Guid profileId, Guid accessPointId, Guid siteId, DateTime now, CancellationToken cancellationToken);
      
    }
}