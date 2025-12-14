using WebServer.Contracts.Infrastructure;
using WebServer.Contracts.Services;
using WebServer.Persistance.Context;
using WebServer.Persistance.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace WebServer.Persistance
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
                                                                IConfiguration configuration)
        {
            // Database Connection
            services.AddSingleton<IConnectionStringProvider, ConnectionStringProvider>();
            services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
            
            // Advisory Lock (동시성 제어 - PostgreSQL Advisory Lock 사용)
            services.AddScoped<IAdvisoryLockService, AdvisoryLockService>();
            services.AddTransient<IAdvisoryLockExecutor, AdvisoryLockExecutor>();

            // Redis Cache
            var config = ConfigurationOptions.Parse("localhost:6379");
            config.Password = "redis";
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(config));
            services.AddScoped<IRedisCacheService, RedisCacheService>();
            
            // Serializer
#if DEBUG
            services.AddScoped<ISerializer, JsonSerializer>();
#else
            services.AddScoped<ISerializer, MessagePackSerializer>();
#endif

            return services;
        }
    }
}
