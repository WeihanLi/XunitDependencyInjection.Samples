namespace MinimalApiTest;

public class ApiTest
{
    private readonly TestWebApplicationFactory _testWebApplicationFactory;

    public ApiTest(TestWebApplicationFactory testWebApplicationFactory)
    {
        _testWebApplicationFactory = testWebApplicationFactory;
    }

    [Fact]
    public async Task HelloTest()
    {
        var client = _testWebApplicationFactory.CreateClient();
        var responseText = await client.GetStringAsync("/");
        Assert.Equal("Hello MinimalAPI", responseText);
    }
}