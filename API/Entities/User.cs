using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class User
    {

        public string FirstName {get; set;}

        public string LastName {get; set;}

        public string Phone {get; set;}
    
        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public virtual ICollection<UserRole> UserRoles {get; set;}

    }
}