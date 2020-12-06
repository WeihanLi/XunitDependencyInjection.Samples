using Microsoft.Extensions.DependencyInjection;
using SharedProject;

namespace BasicUsage
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IIdGenerator, GuidIdGenerator>();
        }
    }
}