using WebServer.Contracts.Services;
using WebServer.Infrastructure.CustomExceptionFilter;
using WebServer.Infrastructure.Errors.Middleware;
using WebServer.Infrastructure.LogEntries;
using WebServer.Infrastructure.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace WebServer.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // Custom Middleware
            services.AddScoped<ExceptionMiddleware>();

            // Custom Filters
            services.AddTransient<LogFilter>();
            services.AddTransient<ExceptionFilter>();

            // 로그 
            services.AddSingleton<ILogService, LogService>();

            return services;
        }
    }
}
