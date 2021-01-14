using System.ComponentModel.DataAnnotations;

namespace AspNetCoreApi.Models.Dto
{
    public class RegisterDto
    {
        [Required]
        public string Role { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required,EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}