using CloudWorks.Api.Endpoints.Requests;
using CloudWorks.Application.Commands.AccessPoints;
using CloudWorks.Application.DTOs.AccessPoints;
using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Services.Contracts.AccessPoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CloudWorks.Api.Endpoints;

[ApiController]
[Route("sites/{siteId:guid}/accessPoints")]
[Authorize]
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
        var userEmail = User.FindFirstValue(ClaimTypes.Email);

        return _accessPointService.OpenAccessPoint(
            new OpenAccessPointCommand() { AccessPointId = accessPointId, ProfileId = request.ProfileId },
            cancellationToken
        );
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