using CloudWorks.Application.DTOs.Profiles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.Queries.Profiles
{
    public record GetProfileByIdQuery(Guid Id) : IRequest<ProfileDTO>;
}
