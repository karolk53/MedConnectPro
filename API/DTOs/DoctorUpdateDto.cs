using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class DoctorUpdateDto
    {   
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName {get; set;}

        [Required]
        public string Phone { get; set; }

        [EmailAddress]
        [Required]
        public string Email {get; set;}   

        [Required]
        public string Gender { get; set; }    
    }
}