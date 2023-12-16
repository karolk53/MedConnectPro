namespace API.DTOs
{
    public class VisitDto
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public string Description { get; set; }
        public DateTime PlannedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
    }
}