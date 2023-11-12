using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class PatientRegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$", ErrorMessage = "Passowrd must have 1 upper case one lower case 1 number 1 non alphanumeric and at least 6 characters")]
        public string Password { get; set; }
    }
}