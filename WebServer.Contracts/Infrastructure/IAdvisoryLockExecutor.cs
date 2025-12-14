using SqlSugar;
using WebServer.Domain.Enums;

namespace WebServer.Contracts.Infrastructure
{
    public interface IAdvisoryLockExecutor
    {
        
        Task<T?> ExecuteWithNamedLockAsync<T>(string lockKey, DatabaseType dbType, Func<ISqlSugarClient, Task<T>> action, int timeoutSeconds = 5);
        Task<T?> ExecuteQueryAsync<T>( DatabaseType dbType, Func<ISqlSugarClient, Task<T>> queryAction);
        Task ExecuteQueryAsync(DatabaseType dbType, Func<ISqlSugarClient, Task> queryAction);
        Task<T?> ExecuteCachedQueryAsync<T>(string cacheKey,DatabaseType dbType,Func<ISqlSugarClient, Task<T>> dbQuery,TimeSpan? cacheDuration = null);
    }
}
