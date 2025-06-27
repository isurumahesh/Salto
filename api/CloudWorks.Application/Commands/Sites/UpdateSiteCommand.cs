using CloudWorks.Application.DTOs.Sites;
using MediatR;

namespace CloudWorks.Application.Commands.Sites
{
    public record UpdateSiteCommand(UpdateSiteDTO UpdateSiteDTO) : IRequest;
}