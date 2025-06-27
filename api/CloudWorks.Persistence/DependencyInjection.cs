using CloudWorks.Persistence.AccessPoints;
using CloudWorks.Persistence.Bookings;
using CloudWorks.Persistence.Schedules;
using CloudWorks.Persistence.Sites;
using CloudWorks.Services.Contracts.AccessPoints;
using CloudWorks.Services.Contracts.Bookings;
using CloudWorks.Services.Contracts.Schedules;
using CloudWorks.Services.Contracts.Sites;
using Microsoft.Extensions.DependencyInjection;

namespace CloudWorks.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceDI(this IServiceCollection services)
        {
            services.AddScoped<ISiteRepository, SiteRepository>();
            services.AddScoped<IAccessPointRepository, AccessPointRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();

            return services;
        }
    }
}