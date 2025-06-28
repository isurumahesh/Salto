using CloudWorks.Api.Endpoints.Requests;
using CloudWorks.Application.Commands.AccessPoints;
using CloudWorks.Application.DTOs.AccessPoints;
using CloudWorks.Application.DTOs.Pagination;
using CloudWorks.Application.Queries.AccessPoints;
using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Services.Contracts.AccessPoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace CloudWorks.Api.Endpoints;

[ApiController]
[Route("sites/{siteId:guid}/accessPoints")]
//[Authorize]
public class AccessPointsController : ControllerBase
{
    private readonly IAccessPointService _accessPointService;
    private readonly IMediator _mediator;

    public AccessPointsController(IAccessPointService accessPointService, IMediator mediator)
    {
        _accessPointService = accessPointService;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get(Guid siteId, [FromQuery] PagingFilter filter, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAccessPointsQuery(siteId, filter), cancellationToken);
        return Ok(result);
    }

    [HttpPost("{accessPointId:guid}/open")]
    public async Task<IActionResult> Open([FromRoute] Guid siteId, [FromRoute] Guid accessPointId, [FromBody] OpenAccessPointRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new AttemptAccessPointCommand(
        new OpenAccessPointCommand() { AccessPointId = accessPointId, ProfileId = request.ProfileId }, siteId, DateTime.UtcNow), cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddAccessPointDTO dto)
    {
        var result = await _mediator.Send(new AddAccessPointCommand(dto));
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAccessPointDTO dto)
    {
        if (id != dto.Id)
        {
            return BadRequest();
        }

        await _mediator.Send(new UpdateAccessPointCommand(dto));
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteAccessPointCommand(id));
        return NoContent();
    }
}