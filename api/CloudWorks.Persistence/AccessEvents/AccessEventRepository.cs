using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Data.Database;
using CloudWorks.Services.Contracts.AccessEvents;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace CloudWorks.Persistence.AccessEvents
{
    public class AccessEventRepository : IAccessEventRepository
    {
        private readonly CloudWorksDbContext _dbContext;

        public AccessEventRepository(CloudWorksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AccessEvent> AddAsync(AccessEvent accessEvent, CancellationToken cancellationToken)
        {
            await _dbContext.AccessEvents.AddAsync(accessEvent);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return accessEvent;
        }

        public async Task<List<AccessEvent>> GetBySiteIdAsync(Guid siteId, CancellationToken cancellationToken)
        {
            return await _dbContext.AccessEvents
                .AsNoTracking()
                .Where(e => e.SiteId == siteId)
                .Include(e => e.AccessPoint)
                .Include(e => e.Profile)
                .AsSplitQuery()
                .OrderByDescending(e => e.Timestamp)
                .ToListAsync(cancellationToken);
        }
    }
}