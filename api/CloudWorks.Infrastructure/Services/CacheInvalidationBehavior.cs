using CloudWorks.Application.Cache;
using CloudWorks.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Infrastructure.Services
{
    public class CacheInvalidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse>
    {
        private readonly ICacheService _cacheService;

        public CacheInvalidationBehavior(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            // First, execute the command
            var response = await next();

            // If command implements ICacheInvalidator, invalidate relevant cache
            if (request is ICacheInvalidator invalidator)
            {
                foreach (var pattern in invalidator.CachePatternsToInvalidate)
                {
                    _cacheService.RemoveByPattern(pattern);
                }
            }

            return response;
        }
    }
}
