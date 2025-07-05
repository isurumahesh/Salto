using CloudWorks.Application.Commands.SiteProfiles;
using CloudWorks.Application.DTOs.SiteProfiles;
using CloudWorks.Application.Queries.SiteProfiles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudWorks.Api.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "UserAccess")]
    [Authorize(Roles = "Administrator")]
    public class SiteProfilesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SiteProfilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddSiteProfile([FromBody] AddSiteProfileDTO dto, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new AddSiteProfileCommand(dto), cancellationToken);
            return CreatedAtAction(nameof(GetByProfileId), new { profileId = result.ProfileId }, result);
        }

        [HttpGet("{siteProfileId:guid}")]
        [ProducesResponseType(typeof(List<SiteProfileDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] Guid siteProfileId, CancellationToken cancellationToken)
        {
            var query = new GetSiteProfileByIdQuery(siteProfileId);
            var profiles = await _mediator.Send(query, cancellationToken);
            return Ok(profiles);
        }

        [HttpGet("/profiles/{profileId:guid}/siteprofiles")]
        [ProducesResponseType(typeof(List<SiteProfileDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByProfileId([FromRoute] Guid profileId, CancellationToken cancellationToken)
        {
            var query = new GetSiteProfileByProfileIdQuery(profileId);
            var profiles = await _mediator.Send(query, cancellationToken);
            return Ok(profiles);
        }

        [HttpGet("/sites/{siteId:guid}/siteprofiles")]
        [ProducesResponseType(typeof(List<SiteProfileDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBySiteId([FromRoute] Guid siteId, CancellationToken cancellationToken)
        {
            var query = new GetSiteProfileBySiteIdQuery(siteId);
            var profiles = await _mediator.Send(query, cancellationToken);
            return Ok(profiles);
        }
    }
}