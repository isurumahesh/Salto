using CloudWorks.Application.DTOs.Sites;
using MediatR;

namespace CloudWorks.Application.Commands.Sites
{
    public record AddSiteCommand(AddSiteDTO AddSiteDTO) : IRequest<Guid>;
}