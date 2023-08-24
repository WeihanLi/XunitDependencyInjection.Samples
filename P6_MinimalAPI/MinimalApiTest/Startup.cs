using Microsoft.Extensions.Hosting;

namespace MinimalApiTest;

public sealed class Startup
{
    public IHostBuilder CreateHostBuilder() => 
        MinimalApiHostBuilderFactory.GetHostBuilder<Program>();
}