using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Data.Contracts.Models;
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

        public async Task<Site?> GetByIdAsync(Guid id) =>
            await _context.Sites.FindAsync(id);

        public IQueryable<Site> Query() => _context.Sites.AsQueryable();

        public async Task AddAsync(Site site) =>
            await _context.Sites.AddAsync(site);

        public Task UpdateAsync(Site site)
        {
            _context.Sites.Update(site);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var site = await GetByIdAsync(id);
            if (site is not null)
                _context.Sites.Remove(site);
        }

        public async Task<IEnumerable<Profile>> GetUsersInSiteAsync(Guid siteId) =>
            await _context.SiteProfiles
                .Where(sp => sp.SiteId == siteId)
                .Include(sp => sp.Profile)
                .Select(sp => sp.Profile)
                .ToListAsync();

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            _context.SaveChangesAsync(cancellationToken);
    }
}