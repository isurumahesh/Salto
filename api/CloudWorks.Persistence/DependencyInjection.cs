using CloudWorks.Persistence.AccessPoints;
using CloudWorks.Persistence.Bookings;
using CloudWorks.Persistence.Sites;
using CloudWorks.Services.Contracts.AccessPoints;
using CloudWorks.Services.Contracts.Bookings;
using CloudWorks.Services.Contracts.Sites;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWorks.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceDI(this IServiceCollection services)
        {
            services.AddScoped<ISiteRepository, SiteRepository>();
            services.AddScoped<IAccessPointRepository, AccessPointRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();

            return services;
        }
    }
}
