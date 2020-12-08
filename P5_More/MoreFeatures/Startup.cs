using Microsoft.Extensions.DependencyInjection;

namespace MoreFeatures
{
    public class Startup
    {
        // add services need to injection
        // ConfigureServices(IServiceCollection services)
        // ConfigureServices(IServiceCollection services, HostBuilderContext hostBuilderContext)
        // ConfigureServices(HostBuilderContext hostBuilderContext, IServiceCollection services)
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHostedService<TestHostedService>()
                ;
        }
    }
}