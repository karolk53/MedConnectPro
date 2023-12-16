using API.DTOs;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //[Route("/api/cards")]
    public class PatientCardsController : BaseApiController
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientCardRepository _patientCardRepository;

        public PatientCardsController(IDoctorRepository doctorRepository, IPatientCardRepository patientCardRepository)
        {
            this._doctorRepository = doctorRepository; 
            this._patientCardRepository = patientCardRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientCardList>>> GetDoctorCards([FromQuery]CardParams cardParams)
        {
            var cards = await _patientCardRepository.GetDoctorCards(User.GetUserId(), cardParams);
            return Ok(cards);
        }
    }
}