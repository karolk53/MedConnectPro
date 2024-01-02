using System.Net;
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
        public async Task RegisterPatient_WhenCorrectData_PatientRegistered()
        {
            var registerData = new PatientRegisterDto {Email = "patient@patient.com", Password="Pa$$w0rd"};
            var content = JsonSerializer.Serialize(registerData);

            var result = await DoPost("/api/patients/account/register",content);
            
            var resultJson = await result.Content.ReadAsStringAsync();
            var resultObj = JsonSerializer.Deserialize<PatientDto>(resultJson, _serializerOptions);

            Assert.True(result.IsSuccessStatusCode);
            Assert.NotNull(resultObj);
            Assert.NotNull(resultObj.Token);
            Assert.Equal(resultObj.Email, registerData.Email);
        }

        [Fact]
        public async Task LoginPatient_WhenCorrectParams_ReturnToken()
        {
            var loginData = new LoginDto {Email = "patient@patient.com", Password="Pa$$w0rd"};
            var content = JsonSerializer.Serialize(loginData);

            var result = await DoPost("/api/patients/account/login", content);

            var resultJson = await result.Content.ReadAsStringAsync();
            var resultObj = JsonSerializer.Deserialize<PatientDto>(resultJson, _serializerOptions);

            Assert.True(result.IsSuccessStatusCode);
            Assert.NotNull(resultObj);
            Assert.NotNull(resultObj.Token);
            Assert.Equal(resultObj.Email, loginData.Email);
            Assert.Equal("application/json", result.Content.Headers.ContentType!.MediaType);
        }

        [Fact]
        public async Task LoginPatient_WhenIncorrectParams_ReturnUnauthorized()
        {
            var loginData = new LoginDto {Email = "patient@patient.com", Password="Pa$$w0ra"};
            var content = JsonSerializer.Serialize(loginData);

            var result = await DoPost("/api/patients/account/login", content);
            Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
        }
    }
}