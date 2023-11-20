using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class OfficesController : BaseApiController
    {
        private readonly IOfficeRepository _officeRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;
        public OfficesController(IOfficeRepository officeRepository, IDoctorRepository doctorRepository, IMapper mapper)
        {
            this._mapper = mapper;
            this._doctorRepository = doctorRepository;
            this._officeRepository = officeRepository;
        }

        [Authorize(Policy = "DoctorOnly")]
        [HttpPost]
        public async Task<ActionResult<Office>> CreateNewOffice(OfficeCreateDto officeDto)
        {
            var doctor = await _doctorRepository.GetDoctorById(User.GetUserId());
            if (doctor == null) return Unauthorized();

            var office = new Office
            {
                Name = officeDto.Name,
                Address = new Address 
                {
                    Street = officeDto.Street,
                    BuildingNumber = officeDto.BuildingNumber,
                    FlatNumber = officeDto.FlatNumber,
                    PostCode = officeDto.PostCode,
                    City = officeDto.City
                }
            };

            _officeRepository.AddNewOffice(office);
            doctor.Office = office;

            if( await _doctorRepository.SaveAllAsync()) return Created(nameof(CreateNewOffice),office);

            return BadRequest("Failed to create office");

        }

        [Authorize(Policy = "DoctorOnly")]
        [HttpPut]
        public async Task<ActionResult<OfficeDto>> EditOffice(OfficeCreateDto officeCreateDto, int officeId)
        {
            var doctor = await _doctorRepository.GetDoctorById(User.GetUserId());
            if(doctor == null) return Unauthorized();

            if(doctor.Office == null) return NotFound("You have to add office first");

            _mapper.Map(officeCreateDto, doctor.Office);

            if(await _officeRepository.SaveAllAsync()) return  NoContent();

            return BadRequest("Failed to update office");

        }
    }
}