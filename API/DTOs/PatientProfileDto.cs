namespace API.DTOs
{
    public class PatientProfileDto
    {
        public string FirstName { get; set; }

        public string LastName {get; set;}

        public string Phone { get; set; }

        public string Email {get; set;}

        public string PESEL {get; set;}

        public DateOnly DateOfBirth { get; set; }

        public AddressDto Address { get; set; }
    }
}