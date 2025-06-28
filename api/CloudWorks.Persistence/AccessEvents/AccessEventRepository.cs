using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Data.Database;
using CloudWorks.Services.Contracts.AccessEvents;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Persistence.AccessEvents
{
    public class AccessEventRepository : IAccessEventRepository
    {
        private readonly CloudWorksDbContext _dbContext;

        public AccessEventRepository(CloudWorksDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(AccessEvent accessEvent)
        {
            await _dbContext.AccessEvents.AddAsync(accessEvent);
        }

        public async Task<List<AccessEvent>> GetBySiteIdAsync(Guid siteId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.AccessEvents
                .Where(e => e.SiteId == siteId)
                .Include(e => e.AccessPoint)
                .Include(e => e.Profile)
                .OrderByDescending(e => e.Timestamp)
                .ToListAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
