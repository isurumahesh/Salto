using CloudWorks.Application.DTOs.AccessEvents;
using MediatR;

namespace CloudWorks.Application.Queries.AccessEvents
{
    public record GetAccessEventsBySiteQuery(Guid SiteId) : IRequest<List<AccessEventDTO>>;
}