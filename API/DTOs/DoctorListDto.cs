namespace API.DTOs
{
    public class DoctorListDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string Gender { get; set; }
        public int NotesCount {get; set;}
        public double TotalRating { get; set; }
    }
}