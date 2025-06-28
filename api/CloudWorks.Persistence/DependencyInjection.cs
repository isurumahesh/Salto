using CloudWorks.Data.Database;
using CloudWorks.Persistence.AccessEvents;
using CloudWorks.Persistence.AccessPoints;
using CloudWorks.Persistence.Bookings;
using CloudWorks.Persistence.Profiles;
using CloudWorks.Persistence.Schedules;
using CloudWorks.Persistence.SiteProfiles;
using CloudWorks.Persistence.Sites;
using CloudWorks.Services.Contracts.AccessEvents;
using CloudWorks.Services.Contracts.AccessPoints;
using CloudWorks.Services.Contracts.Bookings;
using CloudWorks.Services.Contracts.Profiles;
using CloudWorks.Services.Contracts.Schedules;
using CloudWorks.Services.Contracts.SiteProfiles;
using CloudWorks.Services.Contracts.Sites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace CloudWorks.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CloudWorksDbContext>(
            options => options.UseNpgsql(
                GetConnectionString(configuration, "DefaultDb")
            ));

            services.AddScoped<ISiteRepository, SiteRepository>();
            services.AddScoped<IAccessPointRepository, AccessPointRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<ISiteProfileRepository, SiteProfileRepository>();
            services.AddScoped<IAccessEventRepository, AccessEventRepository>();

            return services;
        }

        private static string GetConnectionString(IConfiguration configuration, string key)
        {
            var connectionStringBuilder = new NpgsqlConnectionStringBuilder(configuration.GetConnectionString(key))
            {
                Username = configuration[$"{key}:User"],
                Password = configuration[$"{key}:Password"]
            };

            return connectionStringBuilder.ToString();
        }
    }
}