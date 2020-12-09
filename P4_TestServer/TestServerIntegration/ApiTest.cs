using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TestWebApp.Models;
using Xunit;

namespace TestServerIntegration
{
    public class ApiTest
    {
        private readonly HttpClient _httpClient;

        public ApiTest(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [Fact]
        public async Task ReadyTest()
        {
            using var response = await _httpClient.GetAsync("api/ready");
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task GetTest()
        {
            using var response = await _httpClient.GetAsync("api/test");
            Assert.True(response.IsSuccessStatusCode);

            var responseText = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(responseText);

            var result = JsonSerializer.Deserialize<Result<bool>>(responseText, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
            Assert.NotNull(result);
            Assert.True(result.Data);
        }
    }
}