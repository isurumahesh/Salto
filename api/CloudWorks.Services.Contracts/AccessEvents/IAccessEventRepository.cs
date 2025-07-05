using CloudWorks.Data.Contracts.Entities;

namespace CloudWorks.Services.Contracts.AccessEvents
{
    public interface IAccessEventRepository
    {
        Task<AccessEvent> AddAsync(AccessEvent accessEvent, CancellationToken cancellationToken);

        Task<List<AccessEvent>> GetBySiteIdAsync(Guid siteId, CancellationToken cancellationToken);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}