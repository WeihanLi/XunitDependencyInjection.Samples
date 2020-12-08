using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.DependencyInjection;
using Xunit.DependencyInjection.Logging;

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
            services.AddSingleton<InvokeHelper>();
            services.AddHostedService<TestHostedService>();
        }

        public void Configure(ILoggerFactory loggerFactory, ITestOutputHelperAccessor outputHelperAccessor)
        {
            loggerFactory.AddProvider(new XunitTestOutputLoggerProvider(outputHelperAccessor));
        }
    }
}