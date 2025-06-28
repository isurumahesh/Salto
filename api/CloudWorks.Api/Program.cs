using CloudWorks.Api;
using CloudWorks.Api.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
   options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddServices(builder.Configuration);

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:44366"; // Your IdP base URL
        options.RequireHttpsMetadata = false; // Disable only for dev/testing

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false // Accept any audience
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ManageAccess", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "scwapi:manage");
    });
    options.AddPolicy("UserAccess", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "scwapi:use");
    });
});

builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 100,
                Window = TimeSpan.FromMinutes(1)
            }
        )
    );
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});


builder.Services.AddAppDI(builder.Configuration, builder.Environment);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();
app.MapControllers();

app.Run();

public partial class Program
{ }