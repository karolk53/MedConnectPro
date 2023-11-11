using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DoctorsController : BaseApiController
    {
        private readonly IDoctorRepository _repository;
        private readonly IMapper _mapper;
        private readonly ISpecialisationRepository _specialisationRepository;
        
        public DoctorsController(IDoctorRepository repository, ISpecialisationRepository specialisationRepository ,IMapper mapper)
        {
            this._specialisationRepository = specialisationRepository;
            this._mapper = mapper;
            this._repository = repository;
            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorListDto>>> GetDoctorsListAsync()
        {
            var doctors = await _repository.GetDoctorsListAsync();
            return Ok(doctors);
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<ActionResult<DoctorDto>> GetDoctorProfile(int id)
        {
            var doctor = await _repository.GetDoctorByIdAsync(User.GetUserId());
            return Ok(doctor);
        } 
    }
}