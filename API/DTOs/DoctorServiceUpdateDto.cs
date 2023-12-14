using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class DoctorServiceUpdateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Descripton { get; set; }
        [Required]
        public double Price { get; set; }
    }
}