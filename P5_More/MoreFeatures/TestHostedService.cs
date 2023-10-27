using Microsoft.Extensions.Hosting;

namespace MoreFeatures;

public sealed class TestHostedService : IHostedService
{
    public static bool Started { get; private set; }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Started = true;
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}