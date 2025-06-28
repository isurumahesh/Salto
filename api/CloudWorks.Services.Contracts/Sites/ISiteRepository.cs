using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Data.Contracts.Models;

namespace CloudWorks.Services.Contracts.Sites
{
    public interface ISiteRepository
    {
        Task<Site?> GetByIdAsync(Guid id);

        Task AddAsync(Site site);

        Task UpdateAsync(Site site);

        Task DeleteAsync(Site site);

        Task<IEnumerable<Profile>> GetUsersInSiteAsync(Guid siteId);

        IQueryable<Site> Query();

    }
}