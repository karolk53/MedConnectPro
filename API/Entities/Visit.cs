using API.Helpers;

namespace API.Entities
{
    public class Visit
    {
        public int Id { get; set; }
        public VisitStatus Status { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public DateTime PlannedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}