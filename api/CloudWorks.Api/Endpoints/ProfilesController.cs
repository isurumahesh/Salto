using CloudWorks.Application.Commands.Profiles;
using CloudWorks.Application.DTOs.Profiles;
using CloudWorks.Application.Queries.Profiles;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudWorks.Api.Endpoints
{
    [ApiController]
    [Route("profiles")]
    [Authorize(Policy = "UserAccess")]
    [Authorize(Roles = "Administrator")]
    public class ProfilesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<AddProfileDTO> _addValidator;

        public ProfilesController(IMediator mediator, IValidator<AddProfileDTO> addValidator)
        {
            _mediator = mediator;
            _addValidator = addValidator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] AddProfileDTO request, CancellationToken cancellationToken)
        {
            ValidationResult result = await _addValidator.ValidateAsync(request);

            if (!result.IsValid)
                return BadRequest(result.Errors);

            var command = new AddProfileCommand(request);
            var profile = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { profileId = profile.Id }, profile);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ProfileDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetProfilesQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{profileId:guid}")]
        [ProducesResponseType(typeof(ProfileDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] Guid profileId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetProfileByIdQuery(profileId), cancellationToken);
            return Ok(result);
        }
    }
}