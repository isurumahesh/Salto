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

        public async Task AddAsync(Site site)
        {
            await _context.Sites.AddAsync(site);
            await _context.SaveChangesAsync();
        }
            
        public async Task UpdateAsync(Site site)
        {
            _context.Sites.Update(site);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Site site)
        {
            _context.Sites.Remove(site);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Profile>> GetUsersInSiteAsync(Guid siteId) =>
            await _context.SiteProfiles
                .Where(sp => sp.SiteId == siteId)
                .Include(sp => sp.Profile)
                .Select(sp => sp.Profile)
                .ToListAsync();
    }
}