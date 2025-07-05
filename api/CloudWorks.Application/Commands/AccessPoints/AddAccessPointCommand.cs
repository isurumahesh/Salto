using CloudWorks.Application.DTOs.AccessPoints;
using MediatR;

namespace CloudWorks.Application.Commands.AccessPoints
{
    public record AddAccessPointCommand(AddAccessPointDTO AddAccessPointDTO) : IRequest<AccessPointDTO>;
}