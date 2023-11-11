namespace API.Entities
{
    public class Specialisation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DoctorSpecialisation> DoctorsSpecialisations {get; set;}
    }
}