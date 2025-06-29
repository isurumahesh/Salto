using CloudWorks.Application.Commands.Sites;
using CloudWorks.Application.DTOs.Pagination;
using CloudWorks.Application.DTOs.Sites;
using CloudWorks.Application.Queries.Sites;
using CloudWorks.Data.Contracts.Entities;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudWorks.Api.Endpoints;

[ApiController]
[Route("sites")]
public class SitesController : ControllerBase
{
    private readonly IValidator<AddSiteDTO> _addValidator;
    private readonly IValidator<UpdateSiteDTO> _updateValidator;
    private readonly IMediator _mediator;

    public SitesController(IMediator mediator, IValidator<AddSiteDTO> addValidator, IValidator<UpdateSiteDTO> updateValidator)
    {
        _mediator = mediator;
        _addValidator = addValidator;
        _updateValidator = updateValidator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<SiteDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] PagingFilter pagingFilter, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetSitesQuery(pagingFilter), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SiteDTO), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSiteById(Guid id, CancellationToken cancellationToken)
    {
        var site = await _mediator.Send(new GetSiteByIdQuery(id), cancellationToken);
        if (site == null)
            return NotFound();
        return Ok(site);
    }

    [HttpGet("{id}/users")]
    [ProducesResponseType(typeof(List<Profile>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsersBySiteId(Guid id, CancellationToken cancellationToken)
    {
        var users = await _mediator.Send(new GetUsersInSiteQuery(id), cancellationToken);
        return Ok(users);
    }

    [HttpPost]
    [Authorize(Policy = "ManageAccess")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(AddSiteDTO addSiteDTO, CancellationToken cancellationToken)
    {
        ValidationResult result = await _addValidator.ValidateAsync(addSiteDTO);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        var createdSite = await _mediator.Send(new AddSiteCommand(addSiteDTO));
        return CreatedAtAction(nameof(GetSiteById), new { id = createdSite.Id }, createdSite);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "ManageAccess")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSite(Guid id, [FromBody] UpdateSiteDTO updateSiteDTO)
    {
        if (id != updateSiteDTO.Id)
        {
            return BadRequest();
        }

        ValidationResult result = await _updateValidator.ValidateAsync(updateSiteDTO);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        await _mediator.Send(new UpdateSiteCommand(updateSiteDTO));
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "ManageAccess")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSite(Guid id)
    {
        await _mediator.Send(new DeleteSiteCommand(id));
        return NoContent();
    }
}