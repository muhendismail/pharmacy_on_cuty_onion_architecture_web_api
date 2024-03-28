using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog.Core;
namespace PharmacyOnDuty.Middlewares
{
    public class Logging
    {
        private readonly IConfiguration _configuration;
        private readonly RequestDelegate _next;
        private readonly ILogger<Logging> _logger;
        

        
        public Logging(RequestDelegate next, ILogger<Logging> logger,IConfiguration configuration)
        {
            
            _next = next;
            _logger = logger;
            _configuration=configuration;
        }
       
        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation("Request received");
            await _next(context);
        }
    }

    public static class LoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Logging>();
        }
    }
}
