using CloudWorks.Api.Endpoints.Requests;
using CloudWorks.Application.Commands.Bookings;
using CloudWorks.Application.Queries.Bookings;
using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Services.Contracts.Bookings;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        var serializer = new CalendarSerializer();

        List<Schedule> schedules = new List<Schedule>();

        foreach (var s in request.Schedules)
        {
            var e = new CalendarEvent
            {
                Start = new CalDateTime(s.Start),
                End = new CalDateTime(s.End)
            };

            var calendar = new Ical.Net.Calendar();

            calendar.Events.Add(e);

            schedules.Add(new Schedule()
            {
                Id = Guid.NewGuid(),
                SiteId = siteId,
                Value = serializer.SerializeToString(calendar!)!
            });
        }

        return _bookingService.AddBooking(
            siteId,
            request.Name,
            request.Users,
            request.AccessPoints,
            schedules,
            cancellationToken
        );
    }


    [HttpGet]
   // [Authorize]
    public async Task<IActionResult> GetList()
    {
        var result = await _mediator.Send(new GetBookingsQuery());
        return Ok(result);
    }

    [HttpDelete("{id}")]
   // [Authorize(Policy = "UseAccess")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteBookingCommand(id));
        return NoContent();
    }
}
