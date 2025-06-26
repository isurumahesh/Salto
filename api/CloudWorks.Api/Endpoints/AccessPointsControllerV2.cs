using CloudWorks.Api.Endpoints.Requests;
using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Services.Contracts.AccessPoints;
using Microsoft.AspNetCore.Mvc;

namespace CloudWorks.Api.Endpoints;

[ApiController]
[Route("sites/{siteId:guid}/accessPoints/v2")]
public class AccessPointsControllerV2 : ControllerBase
{
    private readonly IAccessPointService _accessPointService;

    public AccessPointsControllerV2(IAccessPointService accessPointService)
    {
        _accessPointService = accessPointService;
    }

    [HttpGet]
    public Task<List<AccessPoint>> Get(Guid siteId, CancellationToken cancellationToken)
    {
        return _accessPointService.GetAccessPoints(siteId, cancellationToken);
    }

    [HttpPost("{accessPointId:guid}/open")]
    public Task<AccessPointCommandResult<OpenAccessPointCommand>> Open(
        [FromRoute] Guid accessPointId,
        [FromBody] OpenAccessPointRequest request,
        CancellationToken cancellationToken
    )
    {
        return _accessPointService.OpenAccessPoint(
            new OpenAccessPointCommand() { AccessPointId = accessPointId, ProfileId = request.ProfileId },
            cancellationToken
        );
    }
}
