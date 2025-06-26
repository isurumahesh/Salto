using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.Commands.Bookings
{
    public record DeleteBookingCommand(Guid Id) : IRequest;
}
