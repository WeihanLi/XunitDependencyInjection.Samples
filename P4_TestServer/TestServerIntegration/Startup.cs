using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TestServerIntegration
{
    public class Startup
    {
        // custom host build
        public void ConfigureHost(IHostBuilder hostBuilder)
        {
            hostBuilder
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseStartup<TestWebApp.Startup>();

                    builder.UseTestServer();
                    builder.ConfigureServices(services =>
                    {
                        services.AddSingleton(sp => sp.GetRequiredService<IHost>()
                            .GetTestClient()
                        );
                    });
                })
                ;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // ready check
            services.AddHostedService<ReadyCheckHostedService>();
            services.AddSingleton<RedisHealthCheckService>();
            services.AddHostedService(sp => sp.GetRequiredService<RedisHealthCheckService>());
            services.AddHostedService<DbHealthCheckService>();
        }
    }
}