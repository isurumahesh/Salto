using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Data.Database;
using CloudWorks.Services.Contracts.AccessPoints;
using Microsoft.EntityFrameworkCore;

namespace CloudWorks.Services.AccessPoints;

public class AccessPointService : IAccessPointService
{
    private readonly CloudWorksDbContext _cloudWorksDbContext;

    public AccessPointService(CloudWorksDbContext cloudWorksDbContext)
    {
        _cloudWorksDbContext = cloudWorksDbContext;
    }

    public Task<List<AccessPoint>> GetAccessPoints(Guid siteId, CancellationToken cancellationToken)
    {
        return _cloudWorksDbContext.Set<AccessPoint>().Where(x => x.SiteId == siteId).ToListAsync(cancellationToken);
    }

    public Task<AccessPointCommandResult<OpenAccessPointCommand>> OpenAccessPoint(
        OpenAccessPointCommand command,
        CancellationToken cancellationToken
    )
    {
        int magicNumber = new Random(DateTime.Now.Millisecond).Next(0, 1000);
        bool success = magicNumber < 900;

        return Task.FromResult(
            new AccessPointCommandResult<OpenAccessPointCommand>()
            {
                Command = command,
                TimeStamp = DateTime.UtcNow,
                Success = success,
                Error = !success ? "Connection Error" : null
            }
        );
    }
}