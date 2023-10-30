using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class DoctorRegisterDto
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PWZ { get; set; }
    }
}