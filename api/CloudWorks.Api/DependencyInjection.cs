using CloudWorks.Application;
using CloudWorks.Persistence;

namespace CloudWorks.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(this IServiceCollection services)
        {
            services.AddApplicationDI();
            services.AddPersistenceDI();
            return services;
        }
    }
}