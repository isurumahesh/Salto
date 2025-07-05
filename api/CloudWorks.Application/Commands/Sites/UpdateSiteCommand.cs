using CloudWorks.Application.Cache;
using CloudWorks.Application.Constants;
using CloudWorks.Application.DTOs.Sites;
using MediatR;

namespace CloudWorks.Application.Commands.Sites
{
    public record UpdateSiteCommand(UpdateSiteDTO UpdateSiteDTO) : IRequest, ICacheInvalidator
    {
        public IEnumerable<string> CachePatternsToInvalidate
            => new[] { CacheConstants.CacheKeySites };
    }
}