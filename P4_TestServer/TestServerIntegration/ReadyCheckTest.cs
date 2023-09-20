using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace TestServerIntegration;

public class ReadyCheckTest
{
    private readonly RedisHealthCheckService _redisHealthCheckService;
    private readonly HttpClient _httpClient;
    private readonly IServiceProvider _serviceProvider;

    public ReadyCheckTest(RedisHealthCheckService redisHealthCheckService, HttpClient httpClient, IServiceProvider serviceProvider)
    {
        _redisHealthCheckService = redisHealthCheckService;
        _httpClient = httpClient;
        _serviceProvider = serviceProvider;
    }
    
    [Fact]
    public void RedisHealthCheckTest()
    {
        Assert.True(_redisHealthCheckService.IsReady);
    }

    [Fact]
    public void DbHealthCheckTest()
    {
        var dbHealthCheckService = _serviceProvider.GetServices<IHostedService>().OfType<DbHealthCheckService>()
            .First();
        Assert.True(dbHealthCheckService.IsReady);
    }

    [Fact]
    public async Task ApiReadyTest()
    {
        using var response = await _httpClient.GetAsync("api/ready");
        Assert.True(response.IsSuccessStatusCode);
    }
}