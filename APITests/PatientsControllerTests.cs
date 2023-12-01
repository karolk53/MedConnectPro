using System.Net;
using System.Security.Claims;
using API.Controllers;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Moq;

namespace APITests
{
    public class PatientsControllerTests
    {

        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IVisitRepository> _visitMock;
        private readonly Mock<IPatientRepository> _patientsMock;

        public PatientsControllerTests()
        {
            _mapperMock = new Mock<IMapper>();
            _visitMock = new Mock<IVisitRepository>();
            _patientsMock = new Mock<IPatientRepository>();
        }

        [Fact]
        public async Task GetPatient_ReturnsPatient()
        {
            // Arrange
            _patientsMock.Setup(x => x.GetAllPatientsAsync()).Returns(Task.FromResult(new List<PatientProfileDto>().AsEnumerable()));
            var contr = new PatientsController(_patientsMock.Object, _mapperMock.Object, _visitMock.Object);
            //Thread.CurrentPrincipal = new TestPrincipal(new Claim(ClaimTypes.NameIdentifier, "1"));

            // Act
            var viewresult = await contr.GetPatients();

            // Assert
            Assert.IsAssignableFrom<ActionResult<IEnumerable<Patient>>>(viewresult);
        }
    }
}