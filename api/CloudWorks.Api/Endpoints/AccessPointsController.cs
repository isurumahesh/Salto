using CloudWorks.Api.Endpoints.Requests;
using CloudWorks.Application.Commands.AccessPoints;
using CloudWorks.Application.DTOs.AccessPoints;
using CloudWorks.Application.DTOs.Pagination;
using CloudWorks.Application.Queries.AccessPoints;
using CloudWorks.Services.Contracts.AccessPoints;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudWorks.Api.Endpoints;

[ApiController]
[Route("sites/{siteId:guid}/accessPoints")]
public class AccessPointsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<AddAccessPointDTO> _addValidator;
    private readonly IValidator<UpdateAccessPointDTO> _updateValidator;

    public AccessPointsController(IMediator mediator, IValidator<AddAccessPointDTO> addValidator, IValidator<UpdateAccessPointDTO> updateValidator)
    {
        _mediator = mediator;
        _addValidator = addValidator;
        _updateValidator = updateValidator;
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(PagedResult<AccessPointDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromRoute] Guid siteId, [FromQuery] PagingFilter filter, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAccessPointsQuery(siteId, filter), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{accessPointId:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(AccessPointDTO), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById([FromRoute] Guid accessPointId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAccessPointByIdQuery(accessPointId), cancellationToken);
        return Ok(result);
    }

    [HttpPost("{accessPointId:guid}/open")]
    [Authorize]
    [ProducesResponseType(typeof(AccessPointCommandResult<OpenAccessPointCommand>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Open([FromRoute] Guid siteId, [FromRoute] Guid accessPointId, [FromBody] OpenAccessPointDTO request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new AttemptAccessPointCommand(
        new OpenAccessPointCommand() { AccessPointId = accessPointId, ProfileId = request.ProfileId }, siteId, DateTime.UtcNow), cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Policy = "ManageAccess")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] AddAccessPointDTO addAccessPointDTO, CancellationToken cancellationToken)
    {
        ValidationResult result = await _addValidator.ValidateAsync(addAccessPointDTO);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        var accessPoint = await _mediator.Send(new AddAccessPointCommand(addAccessPointDTO));
        return CreatedAtAction(nameof(GetById), new { id = accessPoint.Id }, accessPoint);
    }

    [HttpPut("{accessPointId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] Guid accessPointId, [FromBody] UpdateAccessPointDTO updateAccessPointDTO, CancellationToken cancellationToken)
    {
        if (accessPointId != updateAccessPointDTO.Id)
        {
            return BadRequest();
        }

        ValidationResult result = await _updateValidator.ValidateAsync(updateAccessPointDTO);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        await _mediator.Send(new UpdateAccessPointCommand(updateAccessPointDTO));
        return NoContent();
    }

    [HttpDelete("{accessPointId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid accessPointId)
    {
        await _mediator.Send(new DeleteAccessPointCommand(accessPointId));
        return NoContent();
    }
}