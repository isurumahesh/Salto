using CloudWorks.Data.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Data.Common;

namespace CloudWorks.IntegrationTests.Configuration
{
    public class CustomWebApplicationFactory<TProgram>
      : WebApplicationFactory<TProgram> where TProgram : class
    {
        public string DefaultUserId { get; set; } = "1";
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll<DbContextOptions<CloudWorksDbContext>>();
                services.RemoveAll<DbConnection>();
                          
                var connection = new SqliteConnection("DataSource=:memory:");
                connection.Open();
                services.AddSingleton<DbConnection>(connection);

                services.AddDbContext<CloudWorksDbContext>((container, options) =>
                {
                    var conn = container.GetRequiredService<DbConnection>();
                    options.UseSqlite(conn);
                });
        
                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<CloudWorksDbContext>();
                db.Database.EnsureCreated();
                TestDbSeeder.Seed(db);
                TestDbSeeder.SeedBulkSites(db, 10000);

                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = TestAuthHandler.AuthenticationScheme;
                    options.DefaultScheme = TestAuthHandler.AuthenticationScheme;
                    options.DefaultChallengeScheme = TestAuthHandler.AuthenticationScheme;
                })
                .AddScheme<TestAuthHandlerOptions, TestAuthHandler>(
                    TestAuthHandler.AuthenticationScheme,
                    options =>
                    {
                        options.DefaultUserId = DefaultUserId;
                    });
            });

            builder.UseEnvironment("Test");
        }
    }
}
