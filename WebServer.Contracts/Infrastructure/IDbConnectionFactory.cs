using WebServer.Domain.Enums;
using MySqlConnector;

namespace WebServer.Contracts.Infrastructure
{
    public interface IDbConnectionFactory
    {
        Task<MySqlConnection> CreateConnectionAsync(DatabaseType dbType);
    }
}
