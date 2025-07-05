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
    [Authorize]
    [ProducesResponseType(typeof(PagedResult<SiteDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] PagingFilter pagingFilter, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetSitesQuery(pagingFilter), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{siteId:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(SiteDTO), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSiteById(Guid siteId, CancellationToken cancellationToken)
    {
        var site = await _mediator.Send(new GetSiteByIdQuery(siteId), cancellationToken);
        return Ok(site);
    }

    [HttpGet("{siteId:guid}/users")]
    [ProducesResponseType(typeof(List<Profile>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsersBySiteId(Guid siteId, CancellationToken cancellationToken)
    {
        var users = await _mediator.Send(new GetUsersInSiteQuery(siteId), cancellationToken);
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
        return CreatedAtAction(nameof(GetSiteById), new { siteId = createdSite.Id }, createdSite);
    }

    [HttpPut("{siteId:guid}")]
    [Authorize(Policy = "ManageAccess")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSite([FromRoute] Guid siteId, [FromBody] UpdateSiteDTO updateSiteDTO, CancellationToken cancellationToken)
    {
        if (siteId != updateSiteDTO.Id)
        {
            return BadRequest();
        }

        ValidationResult result = await _updateValidator.ValidateAsync(updateSiteDTO);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        await _mediator.Send(new UpdateSiteCommand(updateSiteDTO));
        return NoContent();
    }

    [HttpDelete("{siteId:guid}")]
    [Authorize(Policy = "ManageAccess")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSite([FromRoute] Guid siteId, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteSiteCommand(siteId));
        return NoContent();
    }
}