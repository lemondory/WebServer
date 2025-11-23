using WebServer.Infrastructure.Errors.Middleware;
using WebServer.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;

namespace WebServer.Infrastructure
{
    public static class Startup
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
            => app
            .UseMiddleware<ExceptionMiddleware>()
            .UseMiddleware<PreRequestMiddleware>();
    }
}
