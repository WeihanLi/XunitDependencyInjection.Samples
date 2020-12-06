using Xunit;

namespace InitializeUsage
{
    public class DelayTest
    {
        private readonly DelayService _delayService;

        public DelayTest(DelayService delayService)
        {
            _delayService = delayService;
        }

        [Fact]
        public void Test()
        {
            Assert.True(_delayService.Ready());

            _delayService.Test();
        }
    }
}