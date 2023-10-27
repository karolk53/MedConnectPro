
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class UserRole : IdentityUserRole<string>
    {
        public string  RoleName { get; set; }
    }
}