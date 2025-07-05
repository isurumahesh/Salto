using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Data.Database;
using CloudWorks.Services.Contracts.Bookings;
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

        public async Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
            await _context.Bookings.FindAsync(id, cancellationToken);

        public IQueryable<Booking> Query() =>
                 _context.Bookings
                .AsNoTracking()
                .AsSplitQuery()
                .Include(b => b.Schedules)
                .Include(b => b.Profiles)
                .Include(b => b.AccessPoints)
                .AsQueryable();

        public async Task<List<Booking>> GetBySiteIdAsync(Guid siteId, CancellationToken cancellationToken) =>
            await _context.Bookings
                .Where(b => b.SiteId == siteId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

        public Task DeleteAsync(Booking booking, CancellationToken cancellationToken)
        {
            _context.Bookings.Remove(booking);
            return Task.CompletedTask;
        }

        public async Task<Booking> AddAsync(Guid siteId, string name, List<Guid> siteProfiles, List<Guid> accessPoints, List<Schedule> schedules, CancellationToken cancellationToken)
        {
            var accessPointsEntities = await _context.AccessPoints
             .Where(x => x.SiteId == siteId && accessPoints.Contains(x.Id))
             .ToListAsync(cancellationToken);

            var siteProfilesEntities = await _context.SiteProfiles
                .Where(x => x.SiteId == siteId && siteProfiles.Contains(x.Id))
                .ToListAsync(cancellationToken);

            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                Name = name,
                SiteId = siteId,
                Profiles = siteProfilesEntities,
                Schedules = schedules,
                AccessPoints = accessPointsEntities
            };

            await _context.Bookings.AddAsync(booking, cancellationToken);

            return booking;
        }

        public async Task<bool> HasValidBookingAsync(Guid profileId, Guid accessPointId, Guid siteId, DateTime nowUtc, CancellationToken cancellationToken)
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

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}