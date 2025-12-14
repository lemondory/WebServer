using WebServer.Domain.Enums;
using SqlSugar;

namespace WebServer.Contracts.Infrastructure
{
    public interface IDbConnectionFactory
    {
        ISqlSugarClient GetClient(DatabaseType dbType);
    }
}
