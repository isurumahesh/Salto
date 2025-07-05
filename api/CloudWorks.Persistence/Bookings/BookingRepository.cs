using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Data.Database;
using CloudWorks.Services.Contracts.Bookings;
using Ical.Net;
using Microsoft.EntityFrameworkCore;

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

        public IQueryable<Booking> Query() =>
                 _context.Bookings
                .AsNoTracking()
                .AsSplitQuery()
                .Include(b => b.Schedules)
                .Include(b => b.Profiles)
                .Include(b => b.AccessPoints)
                .AsQueryable();

        public async Task<IEnumerable<Booking>> GetBySiteIdAsync(Guid siteId) =>
            await _context.Bookings
                .Where(b => b.SiteId == siteId)
                .AsNoTracking()
                .ToListAsync();

        public async Task DeleteAsync(Guid id)
        {
            var booking = await GetByIdAsync(id);
            if (booking is not null)
                _context.Bookings.Remove(booking);
        }

        public async Task<bool> HasValidBookingAsync(
     Guid profileId, Guid accessPointId, Guid siteId, DateTime nowUtc, CancellationToken cancellationToken)
        {
            return await _context.Bookings
                .Include(b => b.Schedules)
                .Include(b => b.AccessPoints)
                .Include(b => b.Profiles)
                    .ThenInclude(sp => sp.Profile)
                .AnyAsync(b =>
                    b.SiteId == siteId &&
                    b.AccessPoints.Any(ap => ap.Id == accessPointId) &&
                    b.Profiles.Any(p => p.Profile.Id == profileId) &&
                    b.Schedules.Any(s => s.StartUtc <= nowUtc && s.EndUtc >= nowUtc),
                    cancellationToken);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            _context.SaveChangesAsync(cancellationToken);
      
    }
}