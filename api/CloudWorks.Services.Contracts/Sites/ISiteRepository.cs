using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Data.Contracts.Models;

namespace CloudWorks.Services.Contracts.Sites
{
    public interface ISiteRepository
    {
        Task<Site?> GetByIdAsync(Guid id);

        Task AddAsync(Site site);

        Task UpdateAsync(Site site);

        Task DeleteAsync(Guid id);

        Task<IEnumerable<Profile>> GetUsersInSiteAsync(Guid siteId);

        Task<PagedResult<Site>> GetSitesAsync(int pageNumber, int pageSize, string? nameFilter, CancellationToken cancellationToken);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}