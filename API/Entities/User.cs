namespace API.Entities
{
        public class User
        {

                public int Id { get; set; }

                public string FirstName { get; set; }

                public string LastName { get; set; }

                public string Phone { get; set; }

                public string Email { get; set; }

                public string Gender { get; set; }

                public byte[] PasswordHash { get; set; }

                public byte[] PasswordSalt { get; set; }

                public string UserRole { get; set; }
        }
}