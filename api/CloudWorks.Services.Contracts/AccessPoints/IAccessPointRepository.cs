using CloudWorks.Data.Contracts.Entities;

namespace CloudWorks.Services.Contracts.AccessPoints
{
    public interface IAccessPointRepository
    {
        Task<AccessPoint?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        IQueryable<AccessPoint> QueryBySiteId(Guid siteId);

        Task<AccessPoint> AddAsync(AccessPoint accessPoint, CancellationToken cancellationToken);
        Task UpdateAsync(AccessPoint accessPoint, CancellationToken cancellationToken);
        Task DeleteAsync(AccessPoint accessPoint, CancellationToken cancellationToken);
    }
}