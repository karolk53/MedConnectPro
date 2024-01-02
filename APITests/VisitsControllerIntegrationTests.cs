using System.Net;
using System.Text.Json;
using API.DTOs;
using FluentAssertions;

namespace APITests
{
    public class VisitsControllerIntegrationTests : HttpClientTest
    {
        public VisitsControllerIntegrationTests(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task RegisterNewVisit_WhenAuthenticated_CreateVisit()
        {
            var visitDto = new VisitAddDto {Description = "Testing visit", PlannedDate="2023-12-29 14:30:00"};
            var content = JsonSerializer.Serialize(visitDto);
            var token = await AcquireToken();

            var result = await DoPostAuthenticated("/api/visits/1", token, content);
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task RegisterNewVisit_WithIncorrectDate_RetrunBadRequest()
        {
            var visitDto = new VisitAddDto {Description = "Testing visit", PlannedDate="2023-12-29 14:00:00"};
            var content = JsonSerializer.Serialize(visitDto);
            var token = await AcquireToken();

            var result = await DoPostAuthenticated("/api/visits/1", token, content);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

    }
}