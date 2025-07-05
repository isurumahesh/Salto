using CloudWorks.Application.DTOs.SiteProfiles;
using CloudWorks.Data.Contracts.Entities;
using MediatR;

namespace CloudWorks.Application.Commands.SiteProfiles
{
    public record AddSiteProfileCommand(AddSiteProfileDTO Dto) : IRequest<SiteProfile>;
}