using CloudWorks.Application.Cache;
using CloudWorks.Application.Constants;
using CloudWorks.Application.DTOs.Sites;
using MediatR;

namespace CloudWorks.Application.Commands.Sites
{
    public record AddSiteCommand(AddSiteDTO AddSiteDTO) : IRequest<SiteDTO>, ICacheInvalidator
    {
        public IEnumerable<string> CachePatternsToInvalidate
            => new[] { CacheConstants.CaccheKeySites };
    }
}