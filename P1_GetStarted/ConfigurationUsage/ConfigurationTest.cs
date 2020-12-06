using Microsoft.Extensions.Configuration;
using Xunit;

namespace ConfigurationUsage
{
    public class ConfigurationTest
    {
        private readonly IConfiguration _configuration;

        public ConfigurationTest(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Fact]
        public void ConfigurationGetTest()
        {
            Assert.NotNull(_configuration);
        }

        [Fact]
        public void ConfigurationGetConnectionStringTest()
        {
            var connString = _configuration.GetConnectionString("Default");
            Assert.NotNull(connString);
            Assert.NotEmpty(connString);
            Assert.Equal("test", connString);
        }
    }
}