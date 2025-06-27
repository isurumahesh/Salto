using CloudWorks.Data.Contracts.Entities;
using MediatR;

namespace CloudWorks.Application.Queries.Sites
{
    public record GetUsersInSiteQuery(Guid SiteId) : IRequest<IEnumerable<Profile>>;
}