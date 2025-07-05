using CloudWorks.Application.DTOs.SiteProfiles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.Queries.SiteProfiles
{
    public record GetSiteProfileByIdQuery(Guid SiteProfileId) : IRequest<SiteProfileDTO>;
}
