using SqlSugar;

namespace WebServer.Contracts.Infrastructure
{
    public interface IAdvisoryLockService
    {
        Task<bool> AcquireLockAsync(ISqlSugarClient client, string lockName, int timeoutSeconds);
        Task<bool> ReleaseLockAsync(ISqlSugarClient client, string lockName);
    }
}