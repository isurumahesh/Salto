using CloudWorks.Data.Contracts.Entities;
using MediatR;

namespace CloudWorks.Application.Queries.Bookings
{
    public record GetBookingsQuery : IRequest<IEnumerable<Booking>>;
}