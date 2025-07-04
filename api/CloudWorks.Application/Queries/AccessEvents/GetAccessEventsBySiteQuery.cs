using CloudWorks.Application.DTOs.AccessEvents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.Queries.AccessEvents
{
    public record GetAccessEventsBySiteQuery(Guid SiteId) : IRequest<List<AccessEventDTO>>;
               
}
