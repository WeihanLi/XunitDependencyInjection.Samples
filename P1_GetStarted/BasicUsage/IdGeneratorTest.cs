using SharedProject;
using Xunit;

namespace BasicUsage
{
    public class IdGeneratorTest
    {
        private readonly IIdGenerator _idGenerator;

        public IdGeneratorTest(IIdGenerator idGenerator)
        {
            _idGenerator = idGenerator;
        }

        [Fact]
        public void NewIdTest()
        {
            var newId = _idGenerator.NewId();
            Assert.NotNull(newId);
            Assert.NotEmpty(newId);
        }
    }
}