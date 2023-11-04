using API.DTOs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DoctorsController : BaseApiController
    {
        private readonly IDoctorRepository _repository;
        private readonly IMapper _mapper;
        
        public DoctorsController(IDoctorRepository repository, IMapper mapper)
        {
            this._mapper = mapper;
            this._repository = repository;
            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorListDto>>> GetDoctorsListAsync()
        {
            var doctors = await _repository.GetDoctorsListAsync();
            return Ok(doctors);
        }


    }
}