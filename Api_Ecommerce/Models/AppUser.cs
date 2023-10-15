using Microsoft.AspNetCore.Identity;

namespace Api_Ecommerce.Models
{
    public class AppUser: IdentityUser
    {
        public string FullName { get; set; }

        public string Address { get; set; }
    }
}
