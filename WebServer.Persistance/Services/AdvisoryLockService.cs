using WebServer.Contracts.Infrastructure;
using SqlSugar;

namespace WebServer.Persistance.Services
{
    public class AdvisoryLockService : IAdvisoryLockService
    {
        public async Task<bool> AcquireLockAsync(ISqlSugarClient client, string lockName, int timeoutSeconds)
        {
            // PostgreSQL advisory lock 사용
            // pg_advisory_lock은 lock_id를 사용 (문자열을 해시로 변환)
            var lockId = Math.Abs(lockName.GetHashCode());
            var startTime = DateTime.UtcNow;
            var timeout = TimeSpan.FromSeconds(timeoutSeconds);

            while (DateTime.UtcNow - startTime < timeout)
            {
                var result = await client.Ado.GetIntAsync(
                    $"SELECT pg_try_advisory_lock({lockId})");
                
                if (result == 1)
                    return true;

                // 락 획득 실패 시 100ms 대기 후 재시도
                await Task.Delay(100);
            }

            return false;
        }

        public async Task<bool> ReleaseLockAsync(ISqlSugarClient client, string lockName)
        {
            // PostgreSQL advisory lock 해제
            var lockId = Math.Abs(lockName.GetHashCode());
            var result = await client.Ado.GetIntAsync(
                $"SELECT pg_advisory_unlock({lockId})");
            return result == 1;
        }
    }
}
