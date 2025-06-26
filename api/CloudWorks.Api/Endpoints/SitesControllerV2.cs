using CloudWorks.Services.Contracts.Sites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudWorks.Api.Endpoints;

[ApiController]
[Route("sites/v2")]
public class SitesControllerV2 : ControllerBase
{
    private readonly ISiteService _siteService;

    public SitesControllerV2(ISiteService siteService)
    {
        _siteService = siteService;
    }

    [HttpGet]
    public Task<List<Site>> Get(CancellationToken cancellationToken)
    {
        return _siteService.GetSites(cancellationToken);
    }
}
