using CloudWorks.Data.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Services.Contracts.Sites
{
    public interface ISiteRepository
    {
        Task<Site?> GetByIdAsync(Guid id);
        Task<List<Site>> GetSites(CancellationToken cancellationToken);
        Task AddAsync(Site site);
        Task UpdateAsync(Site site);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Profile>> GetUsersInSiteAsync(Guid siteId);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
