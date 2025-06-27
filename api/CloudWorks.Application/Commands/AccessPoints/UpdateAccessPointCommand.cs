using CloudWorks.Application.DTOs.AccessPoints;
using MediatR;

namespace CloudWorks.Application.Commands.AccessPoints
{
    public record UpdateAccessPointCommand(UpdateAccessPointDTO UpdateAccessPointDTO) : IRequest;
}