using CloudWorks.Application.Cache;
using CloudWorks.Application.Constants;
using CloudWorks.Application.DTOs.Pagination;
using CloudWorks.Application.DTOs.Sites;
using CloudWorks.Data.Contracts.Models;
using MediatR;

namespace CloudWorks.Application.Queries.Sites
{
    public record GetSitesQuery(PagingFilter PagingFilter) : IRequest<PagedResult<SiteDTO>>, ICachableQuery
    {
        public string CacheKey => $"sites:{PagingFilter.PageNumber}:{PagingFilter.PageSize}:{PagingFilter.Search}";
        public TimeSpan CacheDuration => TimeSpan.FromMinutes(CacheConstants.CacheDurationInMinutes);
    }
}