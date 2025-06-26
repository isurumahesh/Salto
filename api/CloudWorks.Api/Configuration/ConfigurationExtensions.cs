using CloudWorks.Data.Database;
using CloudWorks.Services.AccessPoints;
using CloudWorks.Services.Bookings;
using CloudWorks.Services.Contracts.AccessPoints;
using CloudWorks.Services.Contracts.Bookings;
using CloudWorks.Services.Contracts.Sites;
using CloudWorks.Services.Sites;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace CloudWorks.Api.Configuration;

public static class ConfigurationExtensions
{
    public static void AddDataAccess(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<CloudWorksDbContext>(
            options => options.UseNpgsql(
                GetConnectionString(configuration, "DefaultDb")
            )
        );
    }

    public static void AddServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddScoped<ISiteService, SiteService>();
        services.AddScoped<IAccessPointService, AccessPointService>();
        services.AddScoped<IBookingService, BookingService>();
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
