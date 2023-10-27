using System.ComponentModel.DataAnnotations;
using API.Controllers;

namespace API.DTOs
{
    public class DoctorLoginDto : BaseApiController
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