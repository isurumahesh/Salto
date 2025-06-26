using CloudWorks.Data.Database;
using CloudWorks.Services.Contracts.Sites;
using Microsoft.EntityFrameworkCore;

namespace CloudWorks.Services.Sites;

public sealed class SiteService : ISiteService
{
    private readonly CloudWorksDbContext _cloudWorksDbContext;

    public SiteService(CloudWorksDbContext cloudWorksDbContext)
    {
        _cloudWorksDbContext = cloudWorksDbContext;
    }

    public Task<List<Site>> GetSites(CancellationToken cancellationToken)
    {
        return _cloudWorksDbContext.Set<Site>().ToListAsync(cancellationToken);
    }
}
