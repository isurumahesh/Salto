using MediatR;

namespace CloudWorks.Application.Commands.Sites
{
    public record DeleteSiteCommand(Guid Id) : IRequest;
}