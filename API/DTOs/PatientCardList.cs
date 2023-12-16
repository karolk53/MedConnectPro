namespace API.DTOs
{
    public class PatientCardList
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientPESEL { get; set; }
        public DateOnly CreationDate { get; set; }
        public DateTime LastVisitDate { get; set; }
    }
}