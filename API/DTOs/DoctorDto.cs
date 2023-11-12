namespace API.DTOs
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string PhotoUrl { get; set; }
        public string Gender { get; set; }
        public List<SpecialisationDto> Specialisations { get; set; } = new ();
    }
}