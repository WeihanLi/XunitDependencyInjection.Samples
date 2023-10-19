using Microsoft.Extensions.Logging;
using Xunit;

namespace MoreFeatures
{
    public class FeatureTest
    {
        private readonly InvokeHelper _invokeHelper;
        private readonly ILogger<FeatureTest> _logger;

        public FeatureTest(InvokeHelper invokeHelper, ILogger<FeatureTest> logger)
        {
            _invokeHelper = invokeHelper;
            _logger = logger;
        }

        [Fact]
        public void HostedServiceStartTest()
        {
            Assert.True(TestHostedService.Started);
        }

        [Fact]
        public void OutputHelperAccessorTest()
        {
            _invokeHelper.Profile(() =>
            {
                Thread.Sleep(3000);
            }, nameof(OutputHelperAccessorTest));
        }

        [Fact]
        public void LoggingTest()
        {
            _logger.LogDebug("Debug");
            _logger.LogInformation("Info");
            _logger.LogWarning("Warn");
            _logger.LogError("Error");
            _logger.LogCritical("Critical");
        }
    }
}