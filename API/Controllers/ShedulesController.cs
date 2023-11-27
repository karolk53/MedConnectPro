using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Policy = "DoctorOnly")]
    public class ShedulesController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IDoctorRepository _doctorRepository;
        public ShedulesController(DataContext context, IDoctorRepository doctorRepository)
        {
            this._doctorRepository = doctorRepository;
            this._context = context;
        }

        [HttpPost]
        public async Task<ActionResult> CreateNewShedule(SheduleCreateDto sheduleCreateDto)
        {
            
            var doctor = await _doctorRepository.GetDoctorById(User.GetUserId());
            if(doctor == null) NotFound();

            if(doctor.Office == null) return BadRequest("You have to add office first");

            var hours = new List<TimeOnly>{};
            var startHour = TimeOnly.Parse(sheduleCreateDto.StartHour);
            var endHour = TimeOnly.Parse(sheduleCreateDto.EndHour);
            bool valid = startHour < endHour;

            if(!valid) return BadRequest("Start hour shloudnt be greater than end hour");

            while(startHour < endHour){
                hours.Add(startHour);
                startHour = startHour.AddMinutes(sheduleCreateDto.VisitTime);
            }

            var shedule = new Shedule
            {
                WeekDay = sheduleCreateDto.WeekDay,
                VisitTime = sheduleCreateDto.VisitTime,
                Hours = hours
            };

            doctor.Office.Shedules.Add(shedule);
;
            if( await _context.SaveChangesAsync() > 0) return Ok();

            return BadRequest("Failed to add shedule");
        }

        [HttpDelete("{sheduleId}")]
        public async Task<ActionResult> DeleteShedule(int sheduleId)
        {
            var doctor = await _doctorRepository.GetDoctorById(User.GetUserId());
            if(doctor == null) return Unauthorized();

            var shedule = await _context.Shedules.FindAsync(sheduleId);
            if(!doctor.Office.Shedules.Contains(shedule)) return NotFound();

            _context.Shedules.Remove(shedule);

            if(await _doctorRepository.SaveAllAsync()) return NoContent();
            
            return BadRequest("Failed to delete shedule");
        }

    }
}