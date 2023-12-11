using System.Text.Json;
using API.DTOs;

namespace APITests
{
    public class PatientsControllerIntegrationTests : HttpClientTest
    {
        public PatientsControllerIntegrationTests(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task login()
        {
            var d = new LoginDto { Email = "jan@example.com", Password = "Pa$$w0rd" };
            var content = JsonSerializer.Serialize(d);

            var result = await DoPost("/api/patients/account/login", content);
            Assert.True(result.IsSuccessStatusCode);
            Assert.Equal("application/json", result.Content.Headers.ContentType!.MediaType);
        }
    }
}