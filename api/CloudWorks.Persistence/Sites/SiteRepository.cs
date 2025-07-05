using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Data.Database;
using CloudWorks.Services.Contracts.Sites;
using Microsoft.EntityFrameworkCore;

namespace CloudWorks.Persistence.Sites
{
    public class SiteRepository : ISiteRepository
    {
        private readonly CloudWorksDbContext _context;

        public SiteRepository(CloudWorksDbContext context)
        {
            _context = context;
        }

        public async Task<Site?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
            await _context.Sites.FindAsync(id, cancellationToken);

        public IQueryable<Site> Query() =>
           _context.Sites.AsNoTracking().AsQueryable();

        public async Task AddAsync(Site site, CancellationToken cancellationToken)
        {
            await _context.Sites.AddAsync(site, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Site site, CancellationToken cancellationToken)
        {
            _context.Sites.Update(site);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Site site, CancellationToken cancellationToken)
        {
            _context.Sites.Remove(site);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Profile>> GetUsersInSiteAsync(Guid siteId, CancellationToken cancellationToken)
        {
            return await _context.SiteProfiles
                .Where(sp => sp.SiteId == siteId)
                .Include(sp => sp.Profile)
                .Select(sp => sp.Profile)
                .ToListAsync(cancellationToken);
        }
    }
}