using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface INotesRepository
    {
        void AddNewNote(Note note);
        void DeleteNote(Note note);
        Task<IEnumerable<NoteDto>> GetDoctorsNotesAsync(int doctorId);
        Task<Note> GetNoteById(int noteId);
        Task<bool> SaveAllAsync();
    }
}