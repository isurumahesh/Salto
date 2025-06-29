using CloudWorks.Application.Commands.SiteProfiles;
using CloudWorks.Application.DTOs.SiteProfiles;
using CloudWorks.Application.Queries.SiteProfiles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CloudWorks.Api.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
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

        // GET: api/profiles/{profileId}/siteprofiles
        [HttpGet("api/profiles/{profileId}/siteprofiles")]
        public async Task<IActionResult> GetByProfileId(Guid profileId, CancellationToken cancellationToken)
        {
            var query = new GetSiteProfileByProfileIdQuery(profileId);
            var profile = await _mediator.Send(query, cancellationToken);
            if (profile == null)
                return NotFound();
            return Ok(profile);
        }

        // GET: api/sites/{siteId}/siteprofiles
        [HttpGet("api/sites/{siteId}/siteprofiles")]
        public async Task<IActionResult> GetBySiteId(Guid siteId, CancellationToken cancellationToken)
        {
            var query = new GetSiteProfileBySiteIdQuery(siteId);
            var profile = await _mediator.Send(query, cancellationToken);
            if (profile == null)
                return NotFound();
            return Ok(profile);
        }
    }
}