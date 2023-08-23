using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MinimalApiTest;

public sealed class Startup
{
    public IHostBuilder CreateHostBuilder() => 
        MinimalApiHostBuilderFactory.GetHostBuilder<Program>();

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(sp => sp.GetRequiredService<IHost>()
            .GetTestClient()
        );
    }
}