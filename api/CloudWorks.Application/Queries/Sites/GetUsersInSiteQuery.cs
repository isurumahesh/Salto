using CloudWorks.Data.Contracts.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.Queries.Sites
{
    public record GetUsersInSiteQuery(Guid SiteId) : IRequest<IEnumerable<Profile>>;
}
