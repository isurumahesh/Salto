using CloudWorks.Application;
using CloudWorks.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Json;
using StackExchange.Redis;

namespace CloudWorks.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration config)
        {
            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(config)
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File(new JsonFormatter(), "logs/log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog();
            });

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = config.GetConnectionString("Redis") ?? "localhost:6379";
            });

            services.AddSingleton<IConnectionMultiplexer>(sp =>
                ConnectionMultiplexer.Connect(
                    config.GetConnectionString("Redis") ?? "localhost:6379"
                )
            );

            services.AddScoped<ICacheService, RedisCacheService>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheInvalidationBehavior<,>));

            return services;
        }
    }
}