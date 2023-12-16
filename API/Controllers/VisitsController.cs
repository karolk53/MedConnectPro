using System.Text.Json;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    public class VisitsController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IVisitRepository _visitRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IPatientCardRepository _patientCardRepository;

        public VisitsController(
            IVisitRepository visitRepository, 
            IMapper mapper,
            IPatientRepository patientRepository,
            IEmailSenderService emailSenderService,
            IPatientCardRepository patientCardRepository,
            IDoctorRepository doctorRepository)
        {
            this._visitRepository = visitRepository;
            this._mapper = mapper;
            this._patientRepository = patientRepository;
            this._doctorRepository = doctorRepository;
            this._emailSenderService = emailSenderService;
            this._patientCardRepository = patientCardRepository;
        }

        [Authorize(Policy = "PatientOnly")]
        [HttpPost("{doctorId}")]
        public async Task<ActionResult> RegisterNewVisit(VisitAddDto visitDto,int doctorId)
        {
            var patient = await _patientRepository.GetPatientById(User.GetUserId());
            if(patient == null) return Unauthorized();

            var doctor = await _doctorRepository.GetDoctorById(doctorId);
            if(doctor == null) return NotFound();

            var visitDate = DateTime.Parse(visitDto.PlannedDate);
            if(!ValidDate(visitDate, doctor)) return BadRequest("Invalid date");

            var doctorsVisits = GetDoctorsVisits(doctor);
            if(doctorsVisits.Contains(visitDate)) return BadRequest("Visit date is already registered");

            var visit = new Visit
            {
                Description = visitDto.Description,
                Status = VisitStatus.PLANNED,
                PlannedDate = visitDate,
                Doctor = doctor,
                Patient = patient
            };

            _visitRepository.AddNewVisit(visit);
            if( await _visitRepository.SaveAllAsync()){
                await _emailSenderService.SendVisitRegisteredEmail(patient);
                return Ok();
            } 

            return BadRequest("Failed to add visit");
        }

        [Authorize(Policy = "DoctorPatientOnly")]
        [HttpGet("{visitId}")]
        public async Task<ActionResult<VisitDto>> GetSingleVisit(int visitId)
        {
            var visit = await _visitRepository.GetVisitById(visitId);
            if(visit == null) return NotFound();
            var userId = User.GetUserId();
            if(visit.DoctorId == userId || visit.PatientId == userId) return Ok(_mapper.Map<VisitDto>(visit));
            return BadRequest("Failed to get visit");
        }


        [Authorize(Policy = "DoctorOnly")]
        [HttpPut("start/{visitId}")]
        public async Task<ActionResult> StartVisit(int visitId)
        {

            var doctor = await _doctorRepository.GetDoctorById(User.GetUserId());
            if(doctor == null) return Unauthorized();

            var visit = await _visitRepository.GetVisitById(visitId);
            if(visit == null) return NotFound();

            if(visit.DoctorId != doctor.Id) return BadRequest("You dont have access to this visit");

            if(visit.Status != VisitStatus.PLANNED) return BadRequest("You can only start planned visits");
            visit.Status = VisitStatus.LAST;
            visit.StartDate = DateTime.UtcNow;

            if(doctor.Cards == null) doctor.Cards = new List<PatientCard>();

            if(DoctorDontHavePatient(doctor, visit.PatientId))
            {
                var card = new PatientCard
                {
                    Patient = visit.Patient,
                    Doctor = doctor,
                    CreationDate = DateOnly.FromDateTime(DateTime.Now)
                };
                _patientCardRepository.AddNewCard(card);
            }

            if(await _visitRepository.SaveAllAsync()) return NoContent();
            return BadRequest("Failed to start visit");
        }


        [Authorize(Policy = "DoctorOnly")]
        [HttpPut("end/{visitId}")]
        public async Task<ActionResult> EndVisit(int visitId,[FromBody]JsonElement note)
        {
            var visit = await _visitRepository.GetVisitById(visitId);
            if(visit == null) return NotFound();

            if(visit.DoctorId != User.GetUserId()) return BadRequest("You dont have access to this visit");

            if(visit.Status != VisitStatus.LAST) return BadRequest("You can only end lasting visits");
            visit.Status = VisitStatus.COMPLETED;
            visit.EndDate = DateTime.UtcNow;

            var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(note);
            visit.Note = dict["note"];

            if(await _visitRepository.SaveAllAsync()) return NoContent();
            return BadRequest("Failed to end visit");
        }


        [Authorize(Policy = "DoctorPatientOnly")]
        [HttpPut("cancel/{visitId}")]
        public async Task<ActionResult> CancelVisit(int visitId)
        {
            var visit = await _visitRepository.GetVisitById(visitId);
            if(visit == null) return NotFound();

            if(visit.DoctorId != User.GetUserId() && visit.PatientId != User.GetUserId()) return BadRequest("You dont have access to this visit");

            if(visit.Status != VisitStatus.PLANNED) return BadRequest("You can only cancel planned visits");
            visit.Status = VisitStatus.CANCELED;
            
            if(await _visitRepository.SaveAllAsync()) return NoContent();
            return BadRequest("Failed to cancel visit");
        }

        [Authorize(Policy = "DoctorOnly")]
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<IEnumerable<VisitDto>>> GetPatientVisits([FromQuery]VisitParams visitParams, int patientId)
        {
            var visits = await _visitRepository.GetPatientVisitsListAsync(patientId, visitParams);
            return Ok(visits);
        }

        private bool ValidDate(DateTime visitDate, Doctor doctor)
        {
            var shedules = doctor.Office.Shedules;
            var valid = false;
            foreach(Shedule shedule in shedules)
            {
                if(shedule.WeekDay == visitDate.DayOfWeek.ToString() && shedule.Hours.Contains(TimeOnly.FromTimeSpan(visitDate.TimeOfDay))) valid = true;
            }

            return valid;
        }

        private List<DateTime> GetDoctorsVisits(Doctor doctor)
        {
            List<DateTime> visits = new List<DateTime>();

            foreach(Visit visit in doctor.Visits)
            {
                if(visit.Status == VisitStatus.PLANNED) visits.Add(visit.PlannedDate);
            }

            return visits;
        }
        
        private bool DoctorDontHavePatient(Doctor doctor, int patientId)
        {
            foreach(PatientCard card in doctor.Cards)
            {
                if(card.PatientId == patientId)
                {
                    return false;
                }
            }
            return true;
        }

    }
}