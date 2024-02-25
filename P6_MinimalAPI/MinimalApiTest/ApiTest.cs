using System.Diagnostics;
using MinimalApiSample;
using Xunit;

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
    
    [Fact]
    public async Task ActivityTest()
    {
        var activities = new List<Activity>();
        using var activitySource = new ActivitySource(nameof(ActivityTest));
        
        using var listener = new ActivityListener();
        listener.ShouldListenTo = s => true;
        listener.Sample = (ref ActivityCreationOptions<ActivityContext> _) => ActivitySamplingResult.AllData;
        listener.ActivityStopped = a => activities.Add(a);
        ActivitySource.AddActivityListener(listener);

        using var activity = activitySource.StartActivity();
        var activityId = activity?.Id ?? string.Empty;
        var traceId = activity?.TraceId.ToHexString();
        using var requestMessage = new HttpRequestMessage(HttpMethod.Get, "/activityId");
        requestMessage.Headers.TryAddWithoutValidation("traceparent", activityId);
        using var responseMessage = await _testClient.SendAsync(requestMessage);
        Assert.True(responseMessage.IsSuccessStatusCode);
        var responseText = await responseMessage.Content.ReadAsStringAsync();
        var responseActivityParse = ActivityContext.TryParse(responseText, null, out var activityContext);
        Assert.True(responseActivityParse);                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
        Assert.Equal(traceId, activityContext.TraceId.ToHexString());
    }
}