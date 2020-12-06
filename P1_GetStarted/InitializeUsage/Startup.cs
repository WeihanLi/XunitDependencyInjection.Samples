using Microsoft.Extensions.DependencyInjection;
using System.Threading;

namespace InitializeUsage
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<DelayService>();
        }

        // Initialize logic
        public void Configure(DelayService delayService)
        {
            while (!delayService.Ready())
            {
                Thread.Sleep(200);
            }
        }
    }
}