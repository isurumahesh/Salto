using CloudWorks.Application;
using CloudWorks.Infrastructure;
using CloudWorks.Persistence;

namespace CloudWorks.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            services.AddApplicationDI();
            services.AddPersistenceDI(configuration, environment);
            services.AddInfrastructureDI(configuration);
            return services;
        }
    }
}