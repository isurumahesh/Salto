using CloudWorks.Data.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Services.Contracts.AccessPoints
{
    public interface IAccessPointRepository
    {
        Task<AccessPoint?> GetByIdAsync(Guid id);
        Task<IEnumerable<AccessPoint>> GetBySiteIdAsync(Guid siteId);
        Task AddAsync(AccessPoint accessPoint);
        Task UpdateAsync(AccessPoint accessPoint);
        Task DeleteAsync(Guid id);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
