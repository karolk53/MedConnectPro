using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class VisitAddDto
    {
        [Required]
        public string Note { get; set; }
        [Required]
        public string PlannedDate { get; set; }
    }
}