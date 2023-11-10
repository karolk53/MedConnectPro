namespace API.DTOs
{
    public class DoctorListDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PWZ {get; set;}
        public List<SpecialisationDto> DoctorsSpecialisation { get; set; } = new ();
    }
}