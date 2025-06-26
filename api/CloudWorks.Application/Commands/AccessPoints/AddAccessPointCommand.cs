using CloudWorks.Application.DTOs.AccessPoints;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Application.Commands.AccessPoints
{
    public record AddAccessPointCommand(AddAccessPointDTO AddAccessPointDTO) : IRequest<Guid>;
}
