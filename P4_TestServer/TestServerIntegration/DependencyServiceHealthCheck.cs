using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace TestServerIntegration;

public interface IDependencyServiceHealthCheck : IHostedService
{
    bool IsReady { get; }
}

public abstract class AbstractDependencyService : IDependencyServiceHealthCheck
{
    private volatile bool _isReady;
    public virtual async Task StartAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(WorkDelay, cancellationToken);
        _isReady = true;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    public bool IsReady => _isReady;
    
    protected abstract TimeSpan WorkDelay { get; }
}
