using CloudWorks.Application.DTOs.Bookings;
using CloudWorks.Data.Contracts.Entities;
using MediatR;

namespace CloudWorks.Application.Commands.Bookings
{
    public record AddBookingCommand(Guid SiteId, AddBookingDTO Request) : IRequest<Booking>;
}