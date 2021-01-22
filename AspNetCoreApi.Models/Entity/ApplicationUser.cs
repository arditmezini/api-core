using Microsoft.AspNetCore.Identity;

namespace AspNetCoreApi.Models.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}