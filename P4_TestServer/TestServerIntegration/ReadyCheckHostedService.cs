using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TestServerIntegration
{
    public class ReadyCheckHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ReadyCheckHostedService> _logger;

        public ReadyCheckHostedService(IServiceProvider serviceProvider, ILogger<ReadyCheckHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var client = _serviceProvider.GetRequiredService<HttpClient>();
            while (true)
            {
                using var response = await client.GetAsync("api/ready", cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    break;
                }
                _logger.LogWarning("API has not ready");
                await Task.Delay(1000, cancellationToken);
            }
            _logger.LogInformation("API has ready");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}