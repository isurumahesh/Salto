using CloudWorks.Application.DTOs.Sites;
using CloudWorks.Data.Contracts.Models;
using MediatR;

namespace CloudWorks.Application.Queries.Sites
{
    public class GetSitesQuery : IRequest<PagedResult<SiteDTO>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? NameFilter { get; set; }
    }
}