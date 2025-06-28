using CloudWorks.Data.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Services.Contracts.SiteProfiles
{
    public interface ISiteProfileRepository
    {
        Task AddAsync(SiteProfile siteProfile, CancellationToken cancellationToken);
        Task<List<SiteProfile>> GetBySiteIdAsync(Guid siteId, CancellationToken cancellationToken);
        Task<SiteProfile> GetByProfileIdAsync(Guid profileId, CancellationToken cancellationToken);
    }
}
