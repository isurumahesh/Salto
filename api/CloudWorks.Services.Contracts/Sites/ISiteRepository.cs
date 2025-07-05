using CloudWorks.Data.Contracts.Entities;

namespace CloudWorks.Services.Contracts.Sites
{
    public interface ISiteRepository
    {
        Task<Site?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        IQueryable<Site> Query();

        Task AddAsync(Site site, CancellationToken cancellationToken);

        Task UpdateAsync(Site site, CancellationToken cancellationToken);

        Task DeleteAsync(Site site, CancellationToken cancellationToken);

        Task<IEnumerable<Profile>> GetUsersInSiteAsync(Guid siteId, CancellationToken cancellationToken);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}