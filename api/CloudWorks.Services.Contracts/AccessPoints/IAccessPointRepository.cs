using CloudWorks.Data.Contracts.Entities;

namespace CloudWorks.Services.Contracts.AccessPoints
{
    public interface IAccessPointRepository
    {
        Task<AccessPoint?> GetByIdAsync(Guid id);

        IQueryable<AccessPoint> QueryBySiteId(Guid siteId);

        Task<AccessPoint> AddAsync(AccessPoint accessPoint);

        Task UpdateAsync(AccessPoint accessPoint);

        Task DeleteAsync(AccessPoint accessPoint);

    }
}