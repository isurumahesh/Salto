using CloudWorks.Data.Contracts.Entities;

namespace CloudWorks.Services.Contracts.Bookings
{
    public interface IBookingRepository
    {
        Task<Booking?> GetByIdAsync(Guid id);

        IQueryable<Booking> Query();

        Task<IEnumerable<Booking>> GetBySiteIdAsync(Guid siteId);

        Task DeleteAsync(Guid id);

        Task<bool> HasValidBookingAsync(Guid profileId, Guid accessPointId, Guid siteId, DateTime now, CancellationToken cancellationToken);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}