using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Data.Database;
using CloudWorks.Services.Contracts.AccessPoints;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Persistence.AccessPoints
{
    public class AccessPointRepository : IAccessPointRepository
    {
        private readonly CloudWorksDbContext _context;

        public AccessPointRepository(CloudWorksDbContext context)
        {
            _context = context;
        }

        public async Task<AccessPoint?> GetByIdAsync(Guid id) =>
            await _context.AccessPoints.FindAsync(id);

        public IQueryable<AccessPoint> QueryBySiteId(Guid siteId) =>
      _context.AccessPoints.Where(ap => ap.SiteId == siteId);

        public async Task<AccessPoint> AddAsync(AccessPoint accessPoint)
        {
            await _context.AccessPoints.AddAsync(accessPoint);
            await _context.SaveChangesAsync();
            return accessPoint;
        }

        public async Task UpdateAsync(AccessPoint accessPoint)
        {
            _context.AccessPoints.Update(accessPoint);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AccessPoint accessPoint)
        {
            _context.AccessPoints.Remove(accessPoint);
            await _context.SaveChangesAsync();
        }

    }
}