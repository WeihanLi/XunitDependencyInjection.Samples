using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace TestServerIntegration;

public interface IDependencyServiceHealthCheck : IHostedLifecycleService
{
    bool IsReady { get; }
}

public abstract class AbstractDependencyService : IDependencyServiceHealthCheck
{
    private volatile bool _isReady;

    public virtual Task StartingAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    public virtual async Task StartAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(WorkDelay, cancellationToken);
        _isReady = true;
    }

    public virtual Task StartedAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    public Task StoppingAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    public Task StoppedAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    public bool IsReady => _isReady;
    
    protected abstract TimeSpan WorkDelay { get; }
}
