using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace MinimalApiTest;

public sealed class TestWebApplicationFactory: WebApplicationFactory<Program>
{
    protected override IWebHostBuilder? CreateWebHostBuilder()
    {
        // override default config if needed
        return base.CreateWebHostBuilder();
    }
}