using CloudWorks.Services.Contracts.AccessPoints;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.Commands.AccessPoints
{
    public record AttemptAccessPointCommand(OpenAccessPointCommand OpenAccessPointCommand, Guid SiteId, DateTime Now) 
        : IRequest<AccessPointCommandResult<OpenAccessPointCommand>>;
}
