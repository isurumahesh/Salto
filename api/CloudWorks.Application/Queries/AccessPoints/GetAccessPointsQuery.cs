using CloudWorks.Application.DTOs.AccessPoints;
using CloudWorks.Application.DTOs.Pagination;
using MediatR;

namespace CloudWorks.Application.Queries.AccessPoints
{
    public record GetAccessPointsQuery(Guid SiteId, PagingFilter PagingFilter) : IRequest<PagedResult<AccessPointDTO>>;
}