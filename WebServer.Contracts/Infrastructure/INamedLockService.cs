using MySqlConnector;

namespace WebServer.Contracts.Infrastructure
{
    public interface INamedLockService
    {
        Task<bool> AcquireLockAsync(MySqlConnection connection, string lockName, int timeoutSeconds);
        Task<bool> ReleaseLockAsync(MySqlConnection connection, string lockName);
    }
}