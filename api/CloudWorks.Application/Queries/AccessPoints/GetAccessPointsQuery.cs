using CloudWorks.Application.Cache;
using CloudWorks.Application.Constants;
using CloudWorks.Application.DTOs.AccessPoints;
using CloudWorks.Application.DTOs.Pagination;
using MediatR;

namespace CloudWorks.Application.Queries.AccessPoints
{
    public record GetAccessPointsQuery(Guid SiteId, PagingFilter PagingFilter) : IRequest<PagedResult<AccessPointDTO>>, ICachableQuery
    {
        public string CacheKey => $"accesspoints:{PagingFilter.PageNumber}:{PagingFilter.PageSize}:{PagingFilter.Search}";
        public TimeSpan CacheDuration => TimeSpan.FromMinutes(CacheConstants.CacheDurationInMinutes);
    }

}