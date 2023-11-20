using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ShedulesController : BaseApiController
    {
        private readonly DataContext _context;
        public ShedulesController(DataContext context)
        {
            this._context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Shedule>> CreateNewShedule(SheduleCreateDto sheduleCreateDto)
        {
            

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

            _context.Shedules.Add(shedule);
            if( await _context.SaveChangesAsync() > 0) return Ok(shedule);

            return BadRequest("Failed to add shedule");
        }
    }
}