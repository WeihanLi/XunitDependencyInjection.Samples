using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;
using Xunit.DependencyInjection;

namespace MoreFeatures;

public class HostApplicationBuilderStartupSample(IRandom random)
{
    private static int Counter;
    
    [Fact]
    public void RandomTest()
    {
        var value = random.GetValue(2);
        Assert.True(value is >= 0 and <= 2);
    }

    [Theory]
    [InlineData(null!)]
    public void ConfigurationTest([FromServices]IConfiguration configuration)
    {
        Assert.NotNull(configuration);
        Assert.Equal("World", configuration["Hello"]);
    }

    [Fact]
    public void ConfigureTest()
    {
        Assert.Equal(1, Counter);
    }

    public static class Startup
    {
        public static void ConfigureHostApplicationBuilder(IHostApplicationBuilder builder)
        {
            builder.Configuration.AddInMemoryCollection(
            [
                new KeyValuePair<string, string?>("Hello", "World"),
                new KeyValuePair<string, string?>("CounterInitValue", "1")
            ]);
            if (Convert.ToInt32(builder.Configuration["CounterInitValue"]) > 0)
            {
                builder.Services.AddSingleton<IRandom, RandomService>();
            }
        }

        public static void Configure(IConfiguration configuration)
        {
            Counter = Convert.ToInt32(configuration["CounterInitValue"]);
        }
    }
}

