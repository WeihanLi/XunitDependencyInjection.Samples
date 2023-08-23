using MinimalApiSample;
using Xunit.DependencyInjection;

namespace MinimalApiTest;

public class ApiTest
{
    private readonly HttpClient _testClient;
    private readonly IRandomService _randomService;

    public ApiTest(HttpClient testClient, IRandomService randomService)
    {
        _testClient = testClient;
        _randomService = randomService;
    }

    [Fact]
    public async Task HelloTest()
    {
        var responseText = await _testClient.GetStringAsync("/");
        Assert.Equal("Hello MinimalAPI", responseText);
    }

    [Fact]
    public void ServiceTest()
    {
        var num = _randomService.GetNumber();
        Assert.True(num < 100);
    }
}