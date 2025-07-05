using CloudWorks.Application.Cache;
using CloudWorks.Application.Constants;
using CloudWorks.Application.DTOs.Bookings;
using CloudWorks.Application.DTOs.Pagination;
using MediatR;

namespace CloudWorks.Application.Queries.Bookings
{
    public record GetBookingsQuery(PagingFilter PagingFilter) : IRequest<PagedResult<BookingDTO>>, ICachableQuery
    {
        public string CacheKey => $"bookings:{PagingFilter.PageNumber}:{PagingFilter.PageSize}:{PagingFilter.Search}";
        public TimeSpan CacheDuration => TimeSpan.FromMinutes(CacheConstants.CacheDurationInMinutes);
    }
}