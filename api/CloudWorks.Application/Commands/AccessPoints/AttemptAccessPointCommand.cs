using CloudWorks.Services.Contracts.AccessPoints;
using MediatR;

namespace CloudWorks.Application.Commands.AccessPoints
{
    public record AttemptAccessPointCommand(OpenAccessPointCommand OpenAccessPointCommand, Guid SiteId, DateTime Now)
        : IRequest<AccessPointCommandResult<OpenAccessPointCommand>>;
}