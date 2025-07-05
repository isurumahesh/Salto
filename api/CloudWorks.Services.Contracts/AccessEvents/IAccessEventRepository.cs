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
        Task<AccessEvent> AddAsync(AccessEvent accessEvent, CancellationToken cancellationToken);
        Task<List<AccessEvent>> GetBySiteIdAsync(Guid siteId, CancellationToken cancellationToken);
    }
}
