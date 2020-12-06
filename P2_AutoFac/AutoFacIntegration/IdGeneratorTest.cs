using SharedProject;
using Xunit;

namespace AutoFacIntegration
{
    public class IdGeneratorTest
    {
        private readonly IIdGenerator _idGenerator;

        public IdGeneratorTest(IIdGenerator idGenerator)
        {
            _idGenerator = idGenerator;
        }

        [Fact]
        public void Test()
        {
            Assert.NotNull(_idGenerator);
            Assert.True(_idGenerator is GuidIdGenerator);
            Assert.NotEmpty(_idGenerator.NewId());
        }
    }
}