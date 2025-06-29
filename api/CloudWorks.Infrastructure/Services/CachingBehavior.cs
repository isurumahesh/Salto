using CloudWorks.Application;
using CloudWorks.Application.Cache;
using MediatR;

namespace CloudWorks.Infrastructure.Services
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ICacheService _cacheService;

        public CachingBehavior(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            // Only cache if request implements ICachableQuery
            if (request is ICachableQuery cachable)
            {
                var cached = _cacheService.Get<TResponse>(cachable.CacheKey);
                if (cached != null)
                    return cached;

                var response = await next();

                _cacheService.Set(cachable.CacheKey, response, cachable.CacheDuration);

                return response;
            }

            // Otherwise, just call next
            return await next();
        }
    }
}