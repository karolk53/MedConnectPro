namespace API.Entities
{
    public class DoctorSpecialisation
    {
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public int SpecialisationId { get; set; }
        public Specialisation Specialisation { get; set; }
    }
}