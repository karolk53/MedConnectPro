using System.Text;
using System.Text.Json;
using API.Data;
using API.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace APITests
{
    public abstract class HttpClientTest : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
    {
        
        protected readonly HttpClient _httpClient;
        protected readonly JsonSerializerOptions _serializerOptions;
        protected readonly CustomWebApplicationFactory<Program> _factory;


        public HttpClientTest(CustomWebApplicationFactory<Program> factory)
        {
            this._factory = factory;
            this._httpClient = factory.CreateClient(
                new WebApplicationFactoryClientOptions { AllowAutoRedirect = false }
            );

            this._serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

#pragma warning disable CS4014
            SetupTestData();
#pragma warning restore CS4014
        }

        private async Task SetupTestData()
        {
            using var scope = _factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<DataContext>();

            await Utilities.InitializeDbForTests(db);
        }

        protected async Task<string> AcquireToken()
        {
            var d = new LoginDto { Email = "jan@example.com", Password = "Pa$$w0rd" };
            var content = JsonSerializer.Serialize(d);

            var result = await DoPost("/api/patients/account/login", content);
            Assert.True(result.IsSuccessStatusCode);
            Assert.Equal("application/json", result.Content.Headers.ContentType!.MediaType);

            var resultJson = await result.Content.ReadAsStringAsync();
            var resultObj = JsonSerializer.Deserialize<PatientDto>(resultJson, _serializerOptions);

            Assert.NotNull(resultObj);
            Assert.NotNull(resultObj.Token);

            return resultObj.Token;
        }

        protected async Task<HttpResponseMessage> DoGet(string url)
        {
            return await _httpClient.GetAsync(url); //.ConfigureAwait(false);
        }

        protected async Task<HttpResponseMessage> DoGetAuthenticated(string url, string token)
        {
            var client = _factory.CreateClient();

            if (token != null)
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            
            var result = await client.GetAsync(url); //ConfigureAwait(false);
            return result;
        }

        protected async Task<HttpResponseMessage> DoPostAuthenticated(
            string url,
            string token,
            string content
        )
        {
            var client = _factory.CreateClient();

            if (token != null)
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            return await client
                .PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json"))
                .ConfigureAwait(false);
        }

        protected async Task<HttpResponseMessage> DoPost(string url, string content)
        {
            return await _httpClient
                .PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json"))
                .ConfigureAwait(false);
        }

        protected async Task<HttpResponseMessage> DoPutAuthenticated(string url, string token, string content)
        {
            var client = _factory.CreateClient();
            if (token != null)
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            return await client
                .PutAsync(url, new StringContent(content, Encoding.UTF8, "application/json"))
                .ConfigureAwait(false);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}