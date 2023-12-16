using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class VisitAddDto
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public string PlannedDate { get; set; }
    }
}