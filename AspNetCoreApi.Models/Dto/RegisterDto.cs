using System.ComponentModel.DataAnnotations;

namespace AspNetCoreApi.Models.Dto
{
    public class RegisterDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Password min length", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
