using CloudWorks.Application.Services;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text;
using System.Text.Json;

namespace CloudWorks.Infrastructure.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IConnectionMultiplexer _redis;

        public RedisCacheService(IDistributedCache distributedCache, IConnectionMultiplexer redis)
        {
            _distributedCache = distributedCache;
            _redis = redis;
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

        public void RemoveByPattern(string pattern)
        {
            foreach (var endpoint in _redis.GetEndPoints())
            {
                var server = _redis.GetServer(endpoint);
                var keys = server.Keys(pattern: pattern + "*").ToArray();
                foreach (var key in keys)
                {
                    _distributedCache.Remove(key);
                }
            }
        }
    }
}