using WebServer.Contracts.Infrastructure;
using WebServer.Domain.Enums;
using Microsoft.Extensions.Configuration;
using SqlSugar;

namespace WebServer.Persistance.Context
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _configuration;
        private readonly Dictionary<DatabaseType, ISqlSugarClient> _clients;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            _clients = new Dictionary<DatabaseType, ISqlSugarClient>();
            InitializeClients();
        }

        private void InitializeClients()
        {
            foreach (DatabaseType dbType in Enum.GetValues(typeof(DatabaseType)))
            {
                var connectionString = _configuration.GetConnectionString(dbType.ToString());
                
                if (string.IsNullOrEmpty(connectionString))
                    throw new InvalidOperationException($"Connection string for database '{dbType}' is not configured.");

                var client = new SqlSugarClient(new ConnectionConfig
                {
                    ConnectionString = connectionString,
                    DbType = DbType.PostgreSQL,
                    IsAutoCloseConnection = true,
                    InitKeyType = InitKeyType.Attribute
                });

                // AOP 설정 (쿼리 로깅)
                client.Aop.OnLogExecuting = (sql, pars) =>
                {
                    // 느린 쿼리 감지 및 로깅은 나중에 추가
                };

                _clients[dbType] = client;
            }
        }

        public ISqlSugarClient GetClient(DatabaseType dbType)
        {
            if (!_clients.TryGetValue(dbType, out var client))
            {
                throw new InvalidOperationException($"Database client for '{dbType}' is not configured.");
            }

            return client;
        }
    }
}
