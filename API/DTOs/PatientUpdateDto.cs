using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class PatientUpdateDto
    {
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName {get; set;}

        [Required]
        [RegularExpression("[0-9]{9}")]
        public string Phone { get; set; }

        [EmailAddress]
        [Required]
        public string Email {get; set;}   

        [Required]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        public string Gender { get; set; }    

        [Required]
        public string PESEL {get; set;} 
    }
}