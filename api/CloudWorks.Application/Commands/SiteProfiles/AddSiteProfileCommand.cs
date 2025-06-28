using CloudWorks.Application.DTOs.SiteProfiles;
using CloudWorks.Data.Contracts.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.Commands.SiteProfiles
{
    public record AddSiteProfileCommand(AddSiteProfileDTO Dto) : IRequest<SiteProfile>;
}
