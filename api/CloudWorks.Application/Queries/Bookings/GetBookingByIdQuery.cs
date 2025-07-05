using CloudWorks.Application.DTOs.Bookings;
using MediatR;

namespace CloudWorks.Application.Queries.Bookings
{
    public record GetBookingByIdQuery(Guid Id) : IRequest<BookingDTO>;
}