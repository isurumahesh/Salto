using CloudWorks.Application;
using CloudWorks.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration config)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = config.GetConnectionString("Redis") ?? "localhost:6379";
            });
            services.AddScoped<ICacheService, RedisCacheService>();

            return services;
        }
    }
}
