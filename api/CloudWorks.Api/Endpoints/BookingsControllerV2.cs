using CloudWorks.Api.Endpoints.Requests;
using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Services.Contracts.Bookings;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace CloudWorks.Api.Endpoints;

[ApiController]
[Route("sites/{siteId:guid}/bookings/v2")]
public class BookingsControllerV2 : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsControllerV2(IBookingService bookingService)
    {
        _bookingService = bookingService;
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
}
