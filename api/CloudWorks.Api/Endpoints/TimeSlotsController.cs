using CloudWorks.Application.DTOs.Pagination;
using CloudWorks.Application.DTOs.Schedules;
using CloudWorks.Application.DTOs.Sites;
using CloudWorks.Application.DTOs.TimeSlots;
using CloudWorks.Application.Queries.Schedules;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudWorks.Api.Endpoints
{
    [Route("timeslots")]
    [Authorize(Policy = "UserAccess")]
    [Authorize(Roles = "Administrator")]
    [ApiController]
    public class TimeSlotsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TimeSlotsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("free")]
        [ProducesResponseType(typeof(List<AccessPointTimeSlotDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFreeTimeSlots([FromBody] GetFreeTimeSlotsRequestDTO request, CancellationToken cancellationToken)
        {
            var query = new GetFreeTimeSlotsQuery(request.AccessPointIds, request.Start, request.End, cancellationToken);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("continuous-access")]
        [ProducesResponseType(typeof(List<AccessPointTimeSlotDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserContinuousAccess([FromBody] GetContinuousTimeSlotsRequestDTO request, CancellationToken cancellationToken)
        {
            var query = new GetUserContinuousAccessQuery(request.ProfileId, request.AccessPointIds, request.Start, request.End, cancellationToken);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}