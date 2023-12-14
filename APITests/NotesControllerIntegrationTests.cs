using System.Net;
using System.Text.Json;
using API.DTOs;
using FluentAssertions;

namespace APITests
{
    public class NotesControllerIntegrationTests : HttpClientTest
    {

        public NotesControllerIntegrationTests(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }


        [Fact]
        public async Task CreateNote_WhenAuthenticated_CreateNote()
        {
            var n = new NoteDto{Description = "Test", Value = 1};
            var content = JsonSerializer.Serialize(n);
            var token = await AcquireToken();

            var result = await DoPostAuthenticated("/api/notes/1", token, content);
            Assert.True(result.IsSuccessStatusCode);
        }

        [Fact]
        public async Task CreateNote_WhenNotAuthenticated_ReturnUnauthorized()
        {
            var n = new NoteDto{Description = "Test", Value = 1};
            var content = JsonSerializer.Serialize(n);

            var result = await DoPost("/api/notes/1", content);
            result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GetDoctorsNotes_ForGivenDoctorId_ReturnListOfNotes()
        {
            var doctorId = 1;

            var result = await DoGet("/api/notes/"+doctorId);
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var resultJson = await result.Content.ReadAsStringAsync();
            var resultObj = JsonSerializer.Deserialize<List<NoteDto>>(resultJson, _serializerOptions);

            resultObj.Should().NotBeNull();
            Assert.Equal("Note", resultObj[0].Description);
        }

        [Fact]
        public async Task GetDoctorsNotes_ForGivenBadDoctorId_ReturnNotFound()
        {
            var doctorId = -1;

            var result = await DoGet("/api/notes/"+doctorId);
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}