using CloudWorks.Data.Contracts.Entities;

namespace CloudWorks.Services.Contracts.Bookings
{
    public interface IBookingRepository
    {
        Task<Booking?> GetByIdAsync(Guid id);

        Task<IEnumerable<Booking>> GetAllAsync();

        Task<IEnumerable<Booking>> GetBySiteIdAsync(Guid siteId);

        Task DeleteAsync(Guid id);

        Task<bool> HasValidBookingAsync(string userEmail, Guid accessPointId, Guid siteId, DateTime now, CancellationToken cancellationToken);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}