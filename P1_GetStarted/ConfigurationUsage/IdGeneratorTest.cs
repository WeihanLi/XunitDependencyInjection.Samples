using Microsoft.Extensions.Configuration;
using SharedProject;
using System;
using Xunit;

namespace ConfigurationUsage
{
    public class IdGeneratorTest
    {
        private readonly IConfiguration _configuration;
        private readonly IIdGenerator _idGenerator;

        public IdGeneratorTest(IConfiguration configuration, IIdGenerator idGenerator)
        {
            _configuration = configuration;
            _idGenerator = idGenerator;
        }

        [Fact]
        public void IdTypeTest()
        {
            var isGuid = "Guid".Equals(_configuration["AppSettings:IdType"], StringComparison.OrdinalIgnoreCase);
            if (isGuid)
            {
                Assert.True(_idGenerator is GuidIdGenerator);
            }
            else
            {
                Assert.True(_idGenerator is IntIdGenerator);
            }
        }
    }
}