using CloudWorks.Application.Commands.SiteProfiles;
using CloudWorks.Application.DTOs.SiteProfiles;
using CloudWorks.Application.Queries.SiteProfiles;
using MediatR;
using Microsoft.AspNetCore.Http;
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
      
        [HttpGet("/profiles/{profileId}/siteprofiles")]
        public async Task<IActionResult> GetByProfileId(Guid profileId, CancellationToken cancellationToken)
        {
            var query = new GetSiteProfileByProfileIdQuery(profileId);
            var profile = await _mediator.Send(query, cancellationToken);
            if (profile == null)
                return NotFound();
            return Ok(profile);
        }
     
        [HttpGet("/sites/{siteId}/siteprofiles")]
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