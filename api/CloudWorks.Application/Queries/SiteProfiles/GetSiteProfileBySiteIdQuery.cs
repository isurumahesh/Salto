using CloudWorks.Data.Contracts.Entities;
using MediatR;

namespace CloudWorks.Application.Queries.SiteProfiles
{
    public record GetSiteProfileBySiteIdQuery(Guid SiteId) : IRequest<List<SiteProfile>>;
}