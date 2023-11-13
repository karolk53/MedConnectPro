using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class NotesController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly INotesRepository _notesRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        
        public NotesController(
            INotesRepository notesRepository, 
            IMapper mapper, 
            IPatientRepository patientRepository, 
            IDoctorRepository doctorRepository)
        {
            this._notesRepository = notesRepository;
            this._mapper = mapper;
            this._doctorRepository = doctorRepository;
            this._patientRepository = patientRepository;
        }

        [Authorize(Policy = "PatientOnly")]
        [HttpPost("{doctorId}")]
        public async Task<ActionResult<NoteDto>> AddNewNote(NoteDto noteDto,int doctorId)
        {
            var patient = await _patientRepository.GetPatientById(User.GetUserId());
            if(patient == null) return Unauthorized();

            var doctor = await _doctorRepository.GetDoctorById(doctorId);
            if(doctor == null) return NotFound();

            var note = new Note
            {
                Description = noteDto.Description,
                Value = noteDto.Value,
                Doctor = doctor,
                Patient = patient
            };

            if(doctor.TotalRating == 0.0) 
            {
                doctor.TotalRating = (double) note.Value;
            } else {
                doctor.TotalRating = (doctor.TotalRating + (double) noteDto.Value) / (doctor.Notes.Count() + 1);
            }
            

            _notesRepository.AddNewNote(note);

            if(await _notesRepository.SaveAllAsync()) return Ok();

            return BadRequest();

        }

        [HttpGet("{doctorId}")]
        public async Task<ActionResult<IEnumerable<NoteDto>>> GetDoctorsNotes(int doctorId)
        {
            var doctor = await _doctorRepository.GetDoctorById(doctorId);
            if(doctor == null) return NotFound();

            var notes = await _notesRepository.GetDoctorsNotesAsync(doctorId);

            return Ok(notes);
        }

        [HttpDelete("{noteId}")]
        public async Task<ActionResult> DeleteNote(int noteId)
        {
            var note = await _notesRepository.GetNoteById(noteId);
            if(note == null) return NotFound();

            var doctor = await _doctorRepository.GetDoctorById(note.DoctorId);
            var patient = await _patientRepository.GetPatientById(note.PatientId);

            doctor.Notes.Remove(note);
            if(doctor.Notes.Count() == 0){
                doctor.TotalRating = 0.0;
            } else {
                doctor.TotalRating = (doctor.TotalRating * (doctor.Notes.Count() + 1) - note.Value) / doctor.Notes.Count() ;
            }

            patient.Notes.Remove(note);
            _notesRepository.DeleteNote(note);

            if(await _notesRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to delete note");
        }

    }
}