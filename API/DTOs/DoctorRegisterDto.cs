using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class DoctorRegisterDto
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$", ErrorMessage = "Passowrd must have 1 upper case one lower case 1 number 1 non alphanumeric and at least 6 characters")]
        public string Password { get; set; }

        [Required]
        public string PWZ { get; set; }
    }
}