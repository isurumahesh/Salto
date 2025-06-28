using CloudWorks.Application.Commands.Sites;
using CloudWorks.Application.DTOs.Pagination;
using CloudWorks.Application.DTOs.Sites;
using CloudWorks.Application.Queries.Sites;
using CloudWorks.Services.Contracts.Sites;
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
    private readonly ISiteService _siteService;
    private readonly IValidator<AddSiteDTO> _validator;
    private readonly IMediator _mediator;

    public SitesController(ISiteService siteService, IMediator mediator, IValidator<AddSiteDTO> validator)
    {
        _siteService = siteService;
        _mediator = mediator;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] PagingFilter pagingFilter, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetSitesQuery(pagingFilter), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(AddSiteDTO addSiteDTO, CancellationToken cancellationToken)
    {
        ValidationResult result = await _validator.ValidateAsync(addSiteDTO);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        await _mediator.Send(new AddSiteCommand(addSiteDTO));
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