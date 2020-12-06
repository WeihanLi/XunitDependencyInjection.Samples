using AspectCore.DynamicProxy;
using SharedProject;
using Xunit;

namespace AspectCoreIntegration
{
    public class ConfigurationTest
    {
        private readonly IIdGenerator _idGenerator;

        public ConfigurationTest(IIdGenerator idGenerator)
        {
            _idGenerator = idGenerator;
        }

        [Fact]
        public void InterceptTest()
        {
            Assert.NotNull(_idGenerator);
            Assert.True(_idGenerator.IsProxy());

            Assert.Equal(0, Helper.InterceptCounter);
            var id = _idGenerator.NewId();
            Assert.NotEmpty(id);
            Assert.Equal(1, Helper.InterceptCounter);

            _idGenerator.NewId();
            Assert.Equal(2, Helper.InterceptCounter);

            id = _idGenerator.NewId();
            Assert.Equal(2, Helper.InterceptCounter);
            Assert.Null(id);
        }
    }
}