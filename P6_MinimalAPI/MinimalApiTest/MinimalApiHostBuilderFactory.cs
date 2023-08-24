using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace MinimalApiTest;

public static class MinimalApiHostBuilderFactory
{
    public static IHostBuilder GetHostBuilder<TEntry>(Action<IWebHostBuilder>? configure = null) where TEntry: class
    {
        // var testingAssembly = typeof(WebApplicationFactory<>).Assembly;
        // var hostFactoryResolverType = testingAssembly.GetType("Microsoft.Extensions.Hosting.HostFactoryResolver");
        // ArgumentNullException.ThrowIfNull(hostFactoryResolverType);
        // var resolveHostFactoryMethod = hostFactoryResolverType.GetMethod("ResolveHostFactory", BindingFlags.Static | BindingFlags.Public);
        // ArgumentNullException.ThrowIfNull(resolveHostFactoryMethod);
        // //
        // var deferredHostBuilderType = testingAssembly.GetType("Microsoft.AspNetCore.Mvc.Testing.DeferredHostBuilder");
        // ArgumentNullException.ThrowIfNull(deferredHostBuilderType);
        //
        var entryAssembly = typeof(TEntry).Assembly;
        var deferredHostBuilder = new DeferredHostBuilder();
        deferredHostBuilder.UseEnvironment(Environments.Development);
        // There's no helper for UseApplicationName, but we need to 
        // set the application name to the target entry point 
        // assembly name.
        deferredHostBuilder.ConfigureHostConfiguration(config =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                { HostDefaults.ApplicationKey, entryAssembly.GetName().Name ?? string.Empty }
            });
        });
        // This helper call does the hard work to determine if we can fallback to diagnostic source events to get the host instance
        var factory = HostFactoryResolver.ResolveHostFactory(
            entryAssembly,
            stopApplication: false,
            configureHostBuilder: deferredHostBuilder.ConfigureHostBuilder,
            entrypointCompleted: deferredHostBuilder.EntryPointCompleted);

        ArgumentNullException.ThrowIfNull(factory);
        deferredHostBuilder.SetHostFactory(factory);
        
        deferredHostBuilder.ConfigureWebHost(webHostBuilder =>
        {
            webHostBuilder.UseSolutionRelativeContentRoot(entryAssembly.GetName().Name!);
            configure?.Invoke(webHostBuilder);
            webHostBuilder.UseTestServer();
            webHostBuilder.ConfigureServices(services =>
            {
                services.TryAddSingleton(sp => ((TestServer)sp.GetRequiredService<IServer>())
                    .CreateClient());
            });
        });
        return deferredHostBuilder;
    }
}