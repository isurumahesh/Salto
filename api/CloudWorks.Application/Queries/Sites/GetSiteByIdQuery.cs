using CloudWorks.Application.DTOs.Sites;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.Queries.Sites
{
    public record GetSiteByIdQuery(Guid Id) : IRequest<SiteDTO>;
}
