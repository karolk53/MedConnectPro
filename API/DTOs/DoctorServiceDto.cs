using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class DoctorServiceDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Descripton { get; set; }
        [Required]
        public double Price { get; set; }
        public int DoctorId { get; set; }
    }
}