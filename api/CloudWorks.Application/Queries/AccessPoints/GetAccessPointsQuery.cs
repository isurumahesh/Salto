using CloudWorks.Application.DTOs.AccessPoints;
using MediatR;

namespace CloudWorks.Application.Queries.AccessPoints
{
    public record GetAccessPointsQuery(Guid SiteId) : IRequest<List<AccessPointDTO>>;
}