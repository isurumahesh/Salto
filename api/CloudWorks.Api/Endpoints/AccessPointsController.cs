using CloudWorks.Api.Endpoints.Requests;
using CloudWorks.Application.Commands.AccessPoints;
using CloudWorks.Application.DTOs.AccessPoints;
using CloudWorks.Application.DTOs.Pagination;
using CloudWorks.Application.DTOs.Sites;
using CloudWorks.Application.Queries.AccessPoints;
using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Services.Contracts.AccessPoints;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CloudWorks.Api.Endpoints;

[ApiController]
[Route("sites/{siteId:guid}/accessPoints")]
//[Authorize]
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
    [ProducesResponseType(typeof(PagedResult<AccessPointDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(Guid siteId, [FromQuery] PagingFilter filter, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAccessPointsQuery(siteId, filter), cancellationToken);
        return Ok(result);
    }

    [HttpPost("{accessPointId:guid}/open")]
    [ProducesResponseType(typeof(AccessPointCommandResult<OpenAccessPointCommand>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Open([FromRoute] Guid siteId, [FromRoute] Guid accessPointId, [FromBody] OpenAccessPointRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new AttemptAccessPointCommand(
        new OpenAccessPointCommand() { AccessPointId = accessPointId, ProfileId = request.ProfileId }, siteId, DateTime.UtcNow), cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] AddAccessPointDTO addAccessPointDTO)
    {
        ValidationResult result = await _addValidator.ValidateAsync(addAccessPointDTO);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        await _mediator.Send(new AddAccessPointCommand(addAccessPointDTO));
        return Created();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAccessPointDTO updateAccessPointDTO)
    {
        if (id != updateAccessPointDTO.Id)
        {
            return BadRequest();
        }

        ValidationResult result = await _updateValidator.ValidateAsync(updateAccessPointDTO);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        await _mediator.Send(new UpdateAccessPointCommand(updateAccessPointDTO));
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteAccessPointCommand(id));
        return NoContent();
    }
}