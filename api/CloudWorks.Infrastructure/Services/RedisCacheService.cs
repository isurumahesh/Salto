using CloudWorks.Application;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudWorks.Infrastructure.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;

        public RedisCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public T Get<T>(string key)
        {
            var data = _distributedCache.GetString(key);
            return data is null ? default : JsonSerializer.Deserialize<T>(data);
        }

        public void Set<T>(string key, T value, TimeSpan duration)
        {
            var data = JsonSerializer.Serialize(value);
            _distributedCache.SetString(key, data, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = duration
            });
        }

        public void Remove(string key)
        {
            _distributedCache.Remove(key);
        }
    }
}
