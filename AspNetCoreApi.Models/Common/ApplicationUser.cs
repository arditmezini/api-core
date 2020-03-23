using Microsoft.AspNetCore.Identity;

namespace AspNetCoreApi.Models.Common
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
