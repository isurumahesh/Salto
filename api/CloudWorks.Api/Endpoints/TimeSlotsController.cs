using CloudWorks.Application.Queries.Schedules;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CloudWorks.Api.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSlotsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TimeSlotsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("free")]
        public async Task<IActionResult> GetFreeTimeSlots(
            [FromQuery] List<Guid> accessPointIds,
            [FromQuery] DateTime start,
            [FromQuery] DateTime end)
        {
            var query = new GetFreeTimeSlotsQuery(accessPointIds, start, end);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("continuous-access")]
        public async Task<IActionResult> GetUserContinuousAccess(
            [FromQuery] Guid userId,
            [FromQuery] List<Guid> accessPointIds,
            [FromQuery] DateTime start,
            [FromQuery] DateTime end)
        {
            var query = new GetUserContinuousAccessQuery(userId, accessPointIds, start, end);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}