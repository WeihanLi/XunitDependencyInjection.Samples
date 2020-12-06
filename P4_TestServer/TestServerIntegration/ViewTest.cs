using AngleSharp;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace TestServerIntegration
{
    public class ViewTest
    {
        private readonly HttpClient _httpClient;

        public ViewTest(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [Fact]
        public async Task WelcomeTextTest()
        {
            var response = await _httpClient.GetAsync("");
            Assert.True(response.IsSuccessStatusCode);

            var responseText = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(responseText);

            // https://anglesharp.github.io/docs/Basics.html#first-steps
            //Use the default configuration for AngleSharp
            var config = Configuration.Default;
            //Create a new context for evaluating webpages with the given config
            var context = BrowsingContext.New(config);
            //Create a virtual request to specify the document to load (here from our fixed string)
            var document = await context.OpenAsync(req => req.Content(responseText));

            var welcomeText = document.QuerySelector(".welcome-title")?.TextContent;
            Assert.NotNull(welcomeText);
            Assert.Equal("Welcome", welcomeText);
        }
    }
}