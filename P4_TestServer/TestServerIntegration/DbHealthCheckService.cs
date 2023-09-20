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
    
    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        do
        {
            await Task.Delay(1000, cancellationToken);
        } while (!_redisHealthCheckService.IsReady);
        await base.StartAsync(cancellationToken);
    }

    protected override TimeSpan WorkDelay => TimeSpan.FromSeconds(5);
}