using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Xunit.DependencyInjection;

namespace MoreFeatures
{
    public class TestHostedService : IHostedService
    {
        private readonly ITestOutputHelperAccessor _outputHelperAccessor;

        private readonly Stopwatch _watch = new Stopwatch();

        public TestHostedService(ITestOutputHelperAccessor outputHelperAccessor)
        {
            _outputHelperAccessor = outputHelperAccessor;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _watch.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _watch.Stop();
            _outputHelperAccessor.Output?.WriteLine($"Total test elapsed:{_watch.ElapsedMilliseconds}ms");
            return Task.CompletedTask;
        }
    }
}