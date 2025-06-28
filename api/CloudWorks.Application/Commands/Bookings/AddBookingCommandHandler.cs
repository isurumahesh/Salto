using CloudWorks.Data.Contracts.Entities;
using CloudWorks.Services.Contracts.Bookings;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.Commands.Bookings
{
    public class AddBookingCommandHandler : IRequestHandler<AddBookingCommand, Booking>
    {
        private readonly IBookingService _bookingService;

        public AddBookingCommandHandler(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<Booking> Handle(AddBookingCommand command, CancellationToken cancellationToken)
        {
            var serializer = new CalendarSerializer();
            var schedules = new List<Schedule>();

            foreach (var s in command.Request.Schedules)
            {
                var calendarEvent = new CalendarEvent
                {
                    Start = new CalDateTime(s.StartUtc),
                    End = new CalDateTime(s.EndUtc),
                    Uid = Guid.NewGuid().ToString()
                };

                var calendar = new Ical.Net.Calendar();
                calendar.Events.Add(calendarEvent);

                schedules.Add(new Schedule
                {
                    Id = Guid.NewGuid(),
                    StartUtc= s.StartUtc,
                    EndUtc = s.EndUtc,
                    SiteId = command.SiteId,
                    Value = serializer.SerializeToString(calendar)
                });
            }

            return await _bookingService.AddBooking(
                command.SiteId,
                command.Request.Name,
                command.Request.SiteProfiles,
                command.Request.AccessPoints,
                schedules,
                cancellationToken
            );
        }
    }

}
