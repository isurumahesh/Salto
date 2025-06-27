using MediatR;

namespace CloudWorks.Application.Commands.AccessPoints
{
    public record DeleteAccessPointCommand(Guid Id) : IRequest;
}