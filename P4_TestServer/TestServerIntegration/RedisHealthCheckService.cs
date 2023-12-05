using System;
using System.Threading;
using System.Threading.Tasks;

namespace TestServerIntegration;

public class RedisHealthCheckService : AbstractDependencyService
{
    protected override TimeSpan WorkDelay => TimeSpan.FromSeconds(3);

    public override virtual Task StartingAsync(CancellationToken cancellationToken)
    {
        return base.StartAsync(cancellationToken);
    }

    public override virtual async Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}