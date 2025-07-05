using CloudWorks.Application.Cache;
using CloudWorks.Application.Constants;
using MediatR;

namespace CloudWorks.Application.Commands.Sites
{
    public record DeleteSiteCommand(Guid Id) : IRequest, ICacheInvalidator
    {
        public IEnumerable<string> CachePatternsToInvalidate
            => new[] { CacheConstants.CacheKeySites };
    }
}