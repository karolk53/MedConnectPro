using API.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //[Route("/api/cards")]
    public class PatientCardsController : BaseApiController
    {
        private readonly IDoctorRepository _doctorRepository;

        public PatientCardsController(IDoctorRepository doctorRepository)
        {
            this._doctorRepository = doctorRepository;
            
        }

        [HttpGet]
        public async Task GetDoctorCards()
        {
            //var doctor = 
        }
    }
}