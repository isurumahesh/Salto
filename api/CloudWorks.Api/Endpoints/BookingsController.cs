using CloudWorks.Application.Commands.Bookings;
using CloudWorks.Application.DTOs.Bookings;
using CloudWorks.Application.Queries.Bookings;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudWorks.Api.Endpoints;

[ApiController]
[Route("sites/{siteId:guid}/bookings")]
public class BookingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BookingsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Policy = "BookingsManagePolicy")]
    public async Task<IActionResult> AddBooking(Guid siteId, AddBookingRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new AddBookingCommand(siteId, request), cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var result = await _mediator.Send(new GetBookingsQuery());
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "BookingsManagePolicy")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteBookingCommand(id));
        return NoContent();
    }
}