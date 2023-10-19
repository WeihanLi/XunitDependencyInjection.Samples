using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace MoreFeatures;

public class NestedStartupSample
{
    private readonly IRandom _random;

    public NestedStartupSample(IRandom random)
    {
        _random = random;
    }

    [Fact]
    public void RandomTest()
    {
        var value = _random.GetValue(2);
        Assert.True(value is >= 0 and <= 2);
    }
    
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IRandom, RandomService>();
        }
    }
}

public interface IRandom
{
    int GetValue(int max);
}

public sealed class RandomService : IRandom
{
    public int GetValue(int max) => Random.Shared.Next(max);
}