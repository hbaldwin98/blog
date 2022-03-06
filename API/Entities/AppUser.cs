using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string PublicName { get; set; }
        public ICollection<Article> Articles { get; set; }
        public ICollection<AppUserRole> UserRoles {get; set; }
    }
}