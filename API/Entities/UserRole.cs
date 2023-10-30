
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class UserRole : IdentityUserRole<string>
    {
        public int Id {get; set;}

        public string  RoleName { get; set; }
    }
}