using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class NotesRepository : INotesRepository
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public NotesRepository(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
            
        }

        public void AddNewNote(Note note)
        {
            _context.Notes.Add(note);
        }

        public void DeleteNote(Note note)
        {
            _context.Notes.Remove(note);
        }

        public async Task<IEnumerable<NoteDto>> GetDoctorsNotesAsync(int doctorId)
        {
            return await _context.Notes
                .Where(x => x.DoctorId == doctorId)
                .ProjectTo<NoteDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<Note> GetNoteById(int noteId)
        {
            return await _context.Notes.FindAsync(noteId);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}