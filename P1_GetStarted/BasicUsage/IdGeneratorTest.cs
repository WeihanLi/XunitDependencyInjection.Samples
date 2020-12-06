using SharedProject;
using Xunit;
using Xunit.DependencyInjection;

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

        [Theory]
        [InlineData(null)]
        public void MethodInjectionTest([FromServices] IIdGenerator idGenerator)
        {
            Assert.NotNull(idGenerator);
            Assert.Equal(_idGenerator, idGenerator);
        }
    }
}