using CloudWorks.Application.DTOs.TimeSlots;
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

        [HttpPost("free")]
        public async Task<IActionResult> GetFreeTimeSlots([FromBody] GetFreeTimeSlotsRequestDTO request)
        {
            var query = new GetFreeTimeSlotsQuery(request.AccessPointIds, request.Start, request.End);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("continuous-access")]
        public async Task<IActionResult> GetUserContinuousAccess([FromBody] GetContinuousTimeSlotsRequestDTO request)
        {
            var query = new GetUserContinuousAccessQuery(request.UserId, request.AccessPointIds, request.Start, request.End);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}