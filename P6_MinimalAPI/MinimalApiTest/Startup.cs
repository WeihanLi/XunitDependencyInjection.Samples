using Microsoft.Extensions.DependencyInjection;

namespace MinimalApiTest;

public sealed class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<TestWebApplicationFactory>();
    }
}