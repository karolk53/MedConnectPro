using System.Text.Json;
using API.DTOs;

namespace APITests
{
    public class PatientsControllerIntegrationTests : HttpClientTest
    {
        public PatientsControllerIntegrationTests(CustomWebApplicationFactory<Program> factory) : base(factory) { }
        
        // [Fact]
        // public async Task register()
        // {
        //     var d = new PatientRegisterDto { Email = "patient@example.com", Password = "Pa$$w0rd" };
        //     var content = JsonSerializer.Serialize(d);

        //     var result = await DoPost("/api/patients/account/register", content);
        //     Assert.True(result.IsSuccessStatusCode);
        //     Assert.Equal("application/json", result.Content.Headers.ContentType!.MediaType);
        // }

        [Fact]
        public async Task login()
        {
            var d = new LoginDto { Email = "doctor@doctor.com", Password = "Pa$$w0rd" };
            var content = JsonSerializer.Serialize(d);

            var result = await DoPost("/api/doctors/account/login", content);
            Assert.True(result.IsSuccessStatusCode);
            Assert.Equal("application/json", result.Content.Headers.ContentType!.MediaType);
        }

        // [Fact]
        // public async Task Get_WhenAuthenticated_ReturnsPatient()
        // {
        //     //var token = await AcquireToken();

        //     var d = new LoginDto { Email = "patient@example.com", Password = "Pa$$w0rd" };
        //     var content = JsonSerializer.Serialize(d);

        //     var result = await DoPost("/api/patients/account/login", content);
        //     Assert.True(result.IsSuccessStatusCode);
        //     Assert.Equal("application/json", result.Content.Headers.ContentType!.MediaType);

        //     var resultJson = await result.Content.ReadAsStringAsync();
        //     var resultObj = JsonSerializer.Deserialize<PatientDto>(resultJson, _serializerOptions);
        //     var token = resultObj.Token;

        //     var result1 = await DoGetAuthenticated("/api/patients/me", token);
        //     Assert.True(result1.IsSuccessStatusCode);
        //     //Assert.Equal("application/json", result.Content.Headers.ContentType!.MediaType);
        // }

        [Fact]
        public async Task CreateNote()
        {
            var n = new NoteDto{Description = "Test", Value = 1};
            var content = JsonSerializer.Serialize(n);
            var token = await AcquireToken();

            var result = await DoPostAuthenticated("/api/notes/1", token, content);
            Assert.True(result.IsSuccessStatusCode);
        }
    }
}