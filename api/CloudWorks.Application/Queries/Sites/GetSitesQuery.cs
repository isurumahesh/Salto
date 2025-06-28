using CloudWorks.Application.DTOs.Pagination;
using CloudWorks.Application.DTOs.Sites;
using CloudWorks.Data.Contracts.Models;
using MediatR;

namespace CloudWorks.Application.Queries.Sites
{
    public record GetSitesQuery(PagingFilter PagingFilter) : IRequest<PagedResult<SiteDTO>>;   
}