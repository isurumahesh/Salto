using CloudWorks.Data.Contracts.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.Queries.SiteProfiles
{
    public record GetSiteProfileByProfileIdQuery(Guid ProfileId) : IRequest<SiteProfile>;
}
