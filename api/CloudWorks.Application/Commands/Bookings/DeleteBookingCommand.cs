using MediatR;

namespace CloudWorks.Application.Commands.Bookings
{
    public record DeleteBookingCommand(Guid Id) : IRequest;
}