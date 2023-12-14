using System.Net;
using System.Text.Json;
using API.DTOs;
using FluentAssertions;

namespace APITests
{
    public class DoctorControllerIntegrationTests : HttpClientTest
    {
        public DoctorControllerIntegrationTests(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }


        [Fact]
        public async Task GetDoctorsListAsync_ReturnList()
        {
            var result = await DoGet("/api/doctors");

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var resultJson = await result.Content.ReadAsStringAsync();
            var resultObj = JsonSerializer.Deserialize<List<DoctorDto>>(resultJson, _serializerOptions);

            //Assert.Single(resultObj);
            Assert.Equal("doctor@doctor.com", resultObj[0].Email);
            Assert.Equal("doctor@doctor.com", resultObj[1].Email);
        }


        [Fact]
        public async Task GetDoctorsListAsync_WhenCityDosentMatchAny_ReturnEmptyList()
        {
            var result = await DoGet("/api/doctors?City=Berlin");

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var resultJson = await result.Content.ReadAsStringAsync();
            var resultObj = JsonSerializer.Deserialize<List<DoctorDto>>(resultJson, _serializerOptions);

            Assert.Empty(resultObj);
        }


        [Fact]
        public async Task GetDoctorProfileByPatient_WhenGivenDoctorId_ReturnDoctor()
        {   
            var doctorId = 1;

            var result = await DoGet("/api/doctors/" + doctorId);

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}