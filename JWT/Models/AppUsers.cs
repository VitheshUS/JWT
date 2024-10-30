using Microsoft.AspNetCore.Identity;

namespace JWT.Models
{
    public class AppUsers :IdentityUser
    {
        public string? FullName { get; set; }
    }
}
