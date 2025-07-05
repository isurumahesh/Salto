using CloudWorks.Application.DTOs.SiteProfiles;
using CloudWorks.Data.Contracts.Entities;
using MediatR;

namespace CloudWorks.Application.Queries.SiteProfiles
{
    public record GetSiteProfileByProfileIdQuery(Guid ProfileId) : IRequest<List<SiteProfileDTO>>;
}