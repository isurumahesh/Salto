using CloudWorks.Data.Contracts.Entities;

namespace CloudWorks.Services.Contracts.AccessPoints
{
    public interface IAccessPointRepository
    {
        Task<AccessPoint?> GetByIdAsync(Guid id);

        IQueryable<AccessPoint> QueryBySiteId(Guid siteId);

        Task AddAsync(AccessPoint accessPoint);

        Task UpdateAsync(AccessPoint accessPoint);

        Task DeleteAsync(Guid id);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}