using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Data.Database;
using CloudWorks.Services.Contracts.Bookings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Persistence.Bookings
{
    public class BookingRepository : IBookingRepository
    {
        private readonly CloudWorksDbContext _context;

        public BookingRepository(CloudWorksDbContext context)
        {
            _context = context;
        }

        public async Task<Booking?> GetByIdAsync(Guid id) =>
            await _context.Bookings.FindAsync(id);

        public async Task<IEnumerable<Booking>> GetAllAsync() =>
            await _context.Bookings
                .Include(b => b.Schedules)
                .Include(b => b.Profiles)
                .Include(b => b.AccessPoints)
                .ToListAsync();

        public async Task<IEnumerable<Booking>> GetBySiteIdAsync(Guid siteId) =>
            await _context.Bookings
                .Where(b => b.SiteId == siteId)
                .ToListAsync();

        public async Task DeleteAsync(Guid id)
        {
            var booking = await GetByIdAsync(id);
            if (booking is not null)
                _context.Bookings.Remove(booking);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            _context.SaveChangesAsync(cancellationToken);
    }
}
