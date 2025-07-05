using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Data.Database;
using CloudWorks.Services.Contracts.AccessPoints;
using Microsoft.EntityFrameworkCore;

namespace CloudWorks.Persistence.AccessPoints
{
    public class AccessPointRepository : IAccessPointRepository
    {
        private readonly CloudWorksDbContext _context;

        public AccessPointRepository(CloudWorksDbContext context)
        {
            _context = context;
        }

        public async Task<AccessPoint?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
            await _context.AccessPoints.FindAsync(id, cancellationToken);

        public IQueryable<AccessPoint> QueryBySiteId(Guid siteId)
        {
            return _context.AccessPoints
                .AsNoTracking() 
                .Where(ap => ap.SiteId == siteId);
        }

        public async Task<AccessPoint> AddAsync(AccessPoint accessPoint, CancellationToken cancellationToken)
        {
            await _context.AccessPoints.AddAsync(accessPoint, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return accessPoint;
        }

        public async Task UpdateAsync(AccessPoint accessPoint, CancellationToken cancellationToken)
        {
            _context.AccessPoints.Update(accessPoint);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(AccessPoint accessPoint, CancellationToken cancellationToken)
        {
            _context.AccessPoints.Remove(accessPoint);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}