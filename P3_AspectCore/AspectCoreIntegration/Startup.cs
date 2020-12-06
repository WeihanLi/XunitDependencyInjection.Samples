using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedProject;

namespace AspectCoreIntegration
{
    public class Startup
    {
        // custom host build
        public void ConfigureHost(IHostBuilder hostBuilder)
        {
            hostBuilder
                .UseServiceProviderFactory(new DynamicProxyServiceProviderFactory())
                ;
        }

        // add services need to injection
        // ConfigureServices(IServiceCollection services)
        // ConfigureServices(IServiceCollection services, HostBuilderContext hostBuilderContext)
        // ConfigureServices(HostBuilderContext hostBuilderContext, IServiceCollection services)
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IIdGenerator, GuidIdGenerator>();

            services.ConfigureDynamicProxy(config =>
            {
                config.Interceptors.AddTyped<CounterInterceptor>(Predicates.ForService(nameof(IIdGenerator)));
            });
        }
    }
}