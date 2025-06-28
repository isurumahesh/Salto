using CloudWorks.Api.Endpoints.Requests;
using CloudWorks.Application.Commands.Bookings;
using CloudWorks.Application.DTOs.Bookings;
using CloudWorks.Application.Queries.Bookings;
using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Services.Contracts.Bookings;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CloudWorks.Api.Endpoints;

[ApiController]
[Route("sites/{siteId:guid}/bookings")]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;
    private readonly IMediator _mediator;

    public BookingsController(IBookingService bookingService, IMediator mediator)
    {
        _bookingService = bookingService;
        _mediator = mediator;
    }

    [HttpPost]
    public Task<Booking> AddBooking(
        Guid siteId,
        AddBookingRequest request,
        CancellationToken cancellationToken
    )
    {
        return _mediator.Send(new AddBookingCommand(siteId, request), cancellationToken);
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var result = await _mediator.Send(new GetBookingsQuery());
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteBookingCommand(id));
        return NoContent();
    }
}