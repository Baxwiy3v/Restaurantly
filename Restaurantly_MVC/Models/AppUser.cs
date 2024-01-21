using Microsoft.AspNetCore.Identity;

namespace Restaurantly_MVC.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }

        public string Surname { get; set; }

    }
}
