using CloudWorks.Application.Commands.Profiles;
using CloudWorks.Application.DTOs.Profiles;
using CloudWorks.Services.Contracts.Profiles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CloudWorks.Api.Endpoints
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] AddProfileDTO request,
            CancellationToken cancellationToken)
        {
            var command = new AddProfileCommand(request);
            var result = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetByEmail), new { email = result.Email }, result);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetByEmail(string email, [FromServices] IProfileRepository repo, CancellationToken cancellationToken)
        {
            var profile = await repo.GetByEmailAsync(email, cancellationToken);
            return profile is not null ? Ok(profile) : NotFound();
        }
    }
}
