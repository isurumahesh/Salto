using CloudWorks.Application.DTOs.Bookings;
using CloudWorks.Data.Contracts.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.Commands.Bookings
{
    public record AddBookingCommand(Guid SiteId, AddBookingDTO Request) : IRequest<Booking>;
}