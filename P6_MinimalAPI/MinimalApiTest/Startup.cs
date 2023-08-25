using Microsoft.Extensions.Hosting;
using Xunit.DependencyInjection.AspNetCoreTesting;

namespace MinimalApiTest;

public sealed class Startup
{
    public IHostBuilder CreateHostBuilder() => 
        MinimalApiHostBuilderFactory.GetHostBuilder<Program>();
}