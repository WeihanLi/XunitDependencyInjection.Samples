using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedProject;
using System;

namespace ConfigurationUsage
{
    public class Startup
    {
        // create custom hostBuilder with this method
        //public IHostBuilder CreateHostBuilder()
        //{
        //    return Host.CreateDefaultBuilder();
        //}

        // custom host build
        public void ConfigureHost(IHostBuilder hostBuilder)
        {
            hostBuilder
                .ConfigureHostConfiguration(builder =>
                {
                    builder.AddJsonFile("appsettings.json", true);
                });
        }

        // add services need to injection
        // ConfigureServices(IServiceCollection services)
        // ConfigureServices(IServiceCollection services, HostBuilderContext hostBuilderContext)
        // ConfigureServices(HostBuilderContext hostBuilderContext, IServiceCollection services)
        public void ConfigureServices(IServiceCollection services, HostBuilderContext hostBuilderContext)
        {
            var configuration = hostBuilderContext.Configuration;
            if ("Guid".Equals(configuration["AppSettings:IdType"], StringComparison.OrdinalIgnoreCase))
            {
                services.AddSingleton<IIdGenerator, GuidIdGenerator>();
            }
            else
            {
                services.AddSingleton<IIdGenerator, IntIdGenerator>();
            }
        }
    }
}