using CloudWorks.Data.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Services.Contracts.Bookings
{
    public interface IBookingRepository
    {
        Task<Booking?> GetByIdAsync(Guid id);
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<IEnumerable<Booking>> GetBySiteIdAsync(Guid siteId);
        Task DeleteAsync(Guid id);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
