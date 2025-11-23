using WebServer.Domain.Enums;

namespace WebServer.Contracts.Infrastructure
{
    public interface IConnectionStringProvider
    {
        string GetConnectionString(DatabaseType databaseType);
    }
}