using CloudWorks.Data.Contracts.Entities;

namespace CloudWorks.Services.Contracts.SiteProfiles
{
    public interface ISiteProfileRepository
    {
        Task AddAsync(SiteProfile siteProfile, CancellationToken cancellationToken);

        Task<SiteProfile?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<List<SiteProfile>> GetBySiteIdAsync(Guid siteId, CancellationToken cancellationToken);

        Task<List<SiteProfile>> GetByProfileIdAsync(Guid profileId, CancellationToken cancellationToken);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}