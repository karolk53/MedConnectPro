using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class SpecialisationsController : BaseApiController
    {
        private readonly ISpecialisationRepository _specialisationRepository;
        private readonly IDoctorRepository _doctorRepository;
        public SpecialisationsController(ISpecialisationRepository specialisationRepository, IDoctorRepository doctorRepository)
        {
            this._doctorRepository = doctorRepository;
            this._specialisationRepository = specialisationRepository;
            
        }

        //[Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<ActionResult> CreateSpecialisation(SpecialisationDto specialisationDto)
        {
            var spec = new Specialisation 
            {
                Name = specialisationDto.Name
            };
            _specialisationRepository.AddSpecialisation(spec);

            if( await _specialisationRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to create specialisation");

        }

        [HttpGet]
        public async Task<IEnumerable<SpecialisationDto>> GetSpecialisationsList()
        {
            return await _specialisationRepository.GetSpecialisationsAsync();
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<SpecialisationDto>> GetDoctorsSpecialisation(int id)
        {
            return await _specialisationRepository.GetDoctorsSpecialisations(id);
        }

        [Authorize(Policy = "DoctorOnly")]
        [HttpPost("add/{id}")]
        public async Task<ActionResult<SpecialisationDto>> AddDoctorsSpecialisation(int id)
        {
            var doctor = await _doctorRepository.GetDoctorWithSpecialisation(User.GetUserId());
            var spec = await _specialisationRepository.GetSpecialisationById(id);

            if(doctor == null || spec == null) return NotFound();

            var doctorSpec = new DoctorSpecialisation
            {
                Doctor = doctor,
                Specialisation = spec
            };

            doctor.DoctorsSpecialisations.Add(doctorSpec);

            if(await _specialisationRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to add specialisation");

        }  
    }
}