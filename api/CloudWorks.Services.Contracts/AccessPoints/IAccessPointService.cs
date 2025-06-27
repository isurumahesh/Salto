using CloudWorks.Data.Contracts.Entities;

namespace CloudWorks.Services.Contracts.AccessPoints;

public interface IAccessPointService
{
    Task<AccessPointCommandResult<OpenAccessPointCommand>> OpenAccessPoint(OpenAccessPointCommand command, CancellationToken cancellationToken);

    Task<List<AccessPoint>> GetAccessPoints(Guid siteId, CancellationToken cancellationToken);
}