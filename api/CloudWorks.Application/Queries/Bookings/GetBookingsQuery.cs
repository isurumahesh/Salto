using CloudWorks.Data.Contracts.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.Queries.Bookings
{
    public record GetBookingsQuery : IRequest<IEnumerable<Booking>>;
}
