using CloudWorks.Application.DTOs.Sites;
using MediatR;

namespace CloudWorks.Application.Queries.Sites
{
    public record GetSiteByIdQuery(Guid Id) : IRequest<SiteDTO>;
}