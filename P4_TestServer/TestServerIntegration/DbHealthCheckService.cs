using System;
using System.Threading;
using System.Threading.Tasks;

namespace TestServerIntegration;

public class DbHealthCheckService : AbstractDependencyService
{
    private readonly RedisHealthCheckService _redisHealthCheckService;

    public DbHealthCheckService(RedisHealthCheckService redisHealthCheckService)
    {
        _redisHealthCheckService = redisHealthCheckService;
    }

    protected override TimeSpan WorkDelay => TimeSpan.FromSeconds(5);
}