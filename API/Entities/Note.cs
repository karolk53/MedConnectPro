namespace API.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Value {get; set;}
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}