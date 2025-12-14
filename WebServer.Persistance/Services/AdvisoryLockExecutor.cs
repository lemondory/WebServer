using WebServer.Contracts.Infrastructure;
using WebServer.Domain.Enums;
using SqlSugar;

namespace WebServer.Persistance.Services
{
    public class AdvisoryLockExecutor : IAdvisoryLockExecutor
    {
        private readonly IAdvisoryLockService _advisoryLockService;
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IRedisCacheService _cacheService;

        public AdvisoryLockExecutor(IAdvisoryLockService advisoryLockService, IDbConnectionFactory dbConnectionFactory, IRedisCacheService cacheService)
        {
            _advisoryLockService = advisoryLockService;
            _dbConnectionFactory = dbConnectionFactory;
            _cacheService = cacheService;
        }

        public async Task<T?> ExecuteWithNamedLockAsync<T>(
            string lockKey,
            DatabaseType dbType,
            Func<ISqlSugarClient, Task<T>> action,
            int timeoutSeconds = 5)
        {
            var client = _dbConnectionFactory.GetClient(dbType);

            bool acquired = await _advisoryLockService.AcquireLockAsync(client, lockKey, timeoutSeconds);
            if (!acquired)
                return default;

            try
            {
                return await action(client);
            }
            finally
            {
                await _advisoryLockService.ReleaseLockAsync(client, lockKey);
            }
        }

        public async Task<T?> ExecuteQueryAsync<T>(
            DatabaseType dbType,
            Func<ISqlSugarClient, Task<T>> queryAction)
        {
            var client = _dbConnectionFactory.GetClient(dbType);
            return await queryAction(client);
        }

        public async Task ExecuteQueryAsync(
            DatabaseType dbType,
            Func<ISqlSugarClient, Task> queryAction)
        {
            var client = _dbConnectionFactory.GetClient(dbType);
            await queryAction(client);
        }

        public async Task<T?> ExecuteCachedQueryAsync<T>(string cacheKey,DatabaseType dbType,Func<ISqlSugarClient, Task<T>> dbQuery,TimeSpan? cacheDuration = null)
        {
            // 1. 캐시 조회
            var cached = await _cacheService.GetAsync<T>(cacheKey);
            if (cached != null)
                return cached;

            // 2. 캐시 없으면 DB 조회
            var client = _dbConnectionFactory.GetClient(dbType);
            var result = await dbQuery(client);

            // 3. 캐시에 저장 (기본 10분)
            if (result != null)
            {
                await _cacheService.SetAsync(cacheKey, result, cacheDuration ?? TimeSpan.FromMinutes(10));
            }

            return result;
        }
    }
}