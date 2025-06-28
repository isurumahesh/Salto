using CloudWorks.Data.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Services.Contracts.AccessEvents
{
    public interface IAccessEventRepository
    {
        Task AddAsync(AccessEvent accessEvent);
        Task<List<AccessEvent>> GetBySiteIdAsync(Guid siteId, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
