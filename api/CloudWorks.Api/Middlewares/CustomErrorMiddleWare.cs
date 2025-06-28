using CloudWorks.Application.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace CloudWorks.Api.MiddleWare
{
    public class CustomErrorMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomErrorMiddleWare> _logger;
        private readonly IWebHostEnvironment _env;

        public CustomErrorMiddleWare(RequestDelegate next, ILogger<CustomErrorMiddleWare> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception err)
        {
            var statusCode = err switch
            {
                ArgumentException => HttpStatusCode.BadRequest,
                NotFoundException => HttpStatusCode.NotFound,
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                _ => HttpStatusCode.InternalServerError
            };

            var errorResponse = new
            {
                StatusCode = (int)statusCode,
                Message = err.Message,
                Details = _env.IsDevelopment() ? err.StackTrace : null
            };

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));

            _logger.LogError(err, "An error occurred: {Message}", err.Message);
        }
    }
}