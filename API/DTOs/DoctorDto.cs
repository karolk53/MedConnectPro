
using API.Entities;

namespace API.DTOs
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string Token { get; set; }

        public string PWZ {get; set;}

        public string PhotoUrl { get; set; }

    }
}