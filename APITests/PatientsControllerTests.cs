using API.Controllers;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

            // Act
            var viewresult = await contr.GetPatients();

            // Assert
            Assert.IsAssignableFrom<ActionResult<IEnumerable<Patient>>>(viewresult);
        }
    }
}