using CloudWorks.Application.Commands.Sites;
using CloudWorks.Application.DTOs.Sites;
using CloudWorks.Services.Contracts.Sites;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudWorks.Api.Endpoints;

[ApiController]
[Route("sites")]
public class SitesController : ControllerBase
{
    private readonly ISiteService _siteService;
    private readonly IMediator _mediator;

    public SitesController(ISiteService siteService, IMediator mediator)
    {
        _siteService = siteService;
        _mediator = mediator;
    }

    [HttpGet]
    public Task<List<Site>> Get(CancellationToken cancellationToken)
    {
        return _siteService.GetSites(cancellationToken);
    }

    [HttpPost]
    public async Task<IActionResult> Post(AddSiteDTO site, CancellationToken cancellationToken)
    {
        await _mediator.Send(new AddSiteCommand(site));
        return Ok();
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "ManageAccess")]
    public async Task<IActionResult> UpdateSite(Guid id, [FromBody] UpdateSiteDTO dto)
    {
        if (id != dto.Id)
        {
            return BadRequest();
        }

        await _mediator.Send(new UpdateSiteCommand(dto));
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "ManageAccess")]
    public async Task<IActionResult> DeleteSite(Guid id)
    {   
        await _mediator.Send(new DeleteSiteCommand(id));
        return NoContent();
    }
}
