using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.Commands.AccessPoints
{
    public record DeleteAccessPointCommand(Guid Id) : IRequest;
}
