using CloudWorks.Application.DTOs.Bookings;
using CloudWorks.Data.Contracts.Entities;
using MediatR;

namespace CloudWorks.Application.Queries.Bookings
{
    public record GetBookingByIdQuery(Guid Id) : IRequest<BookingDTO>;
}