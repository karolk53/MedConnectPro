using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.DTOs
{
    public class NoteDto
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(1,5)]
        public int Value {get; set;}
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
    }
}