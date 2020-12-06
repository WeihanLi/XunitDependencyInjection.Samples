using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedProject;

namespace AutoFacIntegration
{
    public class Startup
    {
        // custom host build
        public void ConfigureHost(IHostBuilder hostBuilder)
        {
            hostBuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory(builder =>
            {
                builder.RegisterType<GuidIdGenerator>()
                    .As<IIdGenerator>()
                    .SingleInstance()
                    ;
            }));
        }
    }
}