using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
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
        private readonly IPhotoService _photoService;
        private readonly IPhotoRepository _photoRepository;
        private readonly IDoctorServiceRepository _doctorServiceRepository;
        private readonly IVisitRepository _visitRepository;
        
        public DoctorsController(
            IDoctorRepository repository,
            IPhotoRepository photoRepository,
            ISpecialisationRepository specialisationRepository,
            IPhotoService photoService,
            IDoctorServiceRepository doctorServiceRepository,
            IVisitRepository visitRepository,
            IMapper mapper)
        {
            this._photoRepository = photoRepository;
            this._photoService = photoService;
            this._specialisationRepository = specialisationRepository;
            this._doctorServiceRepository = doctorServiceRepository;
            this._visitRepository = visitRepository;
            this._mapper = mapper;
            this._repository = repository;

        }

        [HttpGet]
        public async Task<ActionResult<PagedList<DoctorListDto>>> GetDoctorsListAsync([FromQuery]DoctorParams doctorParams)
        {
            var doctors = await _repository.GetDoctorsListAsync(doctorParams);
            Response.AddPaginationHeader(new PaginationHeader(doctors.CurrentPage, doctors.TotalCount, doctors.PageSize, doctors.TotalPages));
            return Ok(doctors);
        }


        [Authorize(Policy = "DoctorOnly")]
        [HttpGet("profile")]
        public async Task<ActionResult<DoctorDto>> GetDoctorProfile()
        {
            var doctor = await _repository.GetDoctorByIdAsync(User.GetUserId());
            return Ok(doctor);
        }

        [HttpGet("{doctorId}")]
        public async Task<ActionResult<DoctorDto>> GetDoctorProfileByPatient(int doctorId)
        {
            var doctor = await _repository.GetDoctorByIdAsync(doctorId);

            if(doctor == null) return NotFound("Doctor not found");

            return Ok(doctor);

        }

        [Authorize(Policy = "DoctorOnly")]
        [HttpPut("update")]
        public async Task<ActionResult> UpdateDoctorProfile(DoctorUpdateDto doctorUpdateDto)
        {
            var doctor = await _repository.GetDoctorById(User.GetUserId());

            if(doctor == null) return NotFound();

            _mapper.Map(doctorUpdateDto, doctor);
            if( await _repository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update profile");

        }

        [Authorize(Policy = "DoctorOnly")]
        [HttpPost("photo/add")]
        public async Task<ActionResult<PhotoDto>> AddDoctorPhoto(IFormFile file)
        {
            var doctor = await _repository.GetDoctorById(User.GetUserId());
            if(doctor == null) return NotFound();

            if(doctor.Photo != null){
                var r = await _photoService.DeletePhotoAsync(doctor.Photo.PublicId);
                if(r.Error != null) return BadRequest(r.Error.Message);
            }  

            var result = await _photoService.AddPhotoAsync(file);
            if(result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo 
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            doctor.Photo = photo;

            if(await _repository.SaveAllAsync()) return CreatedAtAction(nameof(GetDoctorProfile), new {id = doctor.Id}, _mapper.Map<PhotoDto>(photo));

            return BadRequest("Failed to add photo");


        }

        [Authorize(Policy = "DoctorOnly")]
        [HttpDelete("photo/delete")]
        public async Task<ActionResult> DeletePhoto()
        {
            var doctor = await _repository.GetDoctorById(User.GetUserId());
            var photo = doctor.Photo;

            if(doctor == null) return Unauthorized();
            if(photo == null) return NotFound("Photo not found");

            if(photo.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if(result.Error != null) return BadRequest(result.Error.Message);
            }

            doctor.Photo = null;
            _photoRepository.DeletePhoto(photo);
            if(await _repository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to delete photo");

        }

        [Authorize(Policy = "DoctorOnly")]
        [HttpPost("services")]
        public async Task<ActionResult<DoctorServiceDto>> AddNewDoctorService(DoctorServiceDto serviceDto)
        {
            var doctor = await _repository.GetDoctorById(User.GetUserId());

            if(doctor == null) return Unauthorized();

            var service = new DoctorService
            {
                Name = serviceDto.Name,
                Descripton = serviceDto.Descripton,
                Price = serviceDto.Price,
                Doctor = doctor
            };

            _doctorServiceRepository.CreateNewService(service);

            if(await _repository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to add service");

        }

        [Authorize(Policy = "DoctorOnly")]
        [HttpDelete("services/delete/{servId}")]
        public async Task<ActionResult> DeleteDoctorService(int servId)
        {
            var doctor = await _repository.GetDoctorById(User.GetUserId());

            if(doctor == null) return Unauthorized();

            var service = await _doctorServiceRepository.GetDoctorService(servId);
            if(service == null) return NotFound();
            if(doctor.DoctorServices.Contains(service)) 
            {
                _doctorServiceRepository.DeleteService(service);
                doctor.DoctorServices.Remove(service);
                if( await _repository.SaveAllAsync()) return NoContent();
            }

            return BadRequest("Failed to delete service");

        }

        [Authorize(Policy = "DoctorOnly")]
        [HttpGet("visits")]
        public async Task<ActionResult<IEnumerable<VisitDto>>> GetDoctorsVisitsList()
        {
            var visits = await _visitRepository.GetDoctorVisitsList(User.GetUserId());
            return Ok(visits);
        }
    }
}