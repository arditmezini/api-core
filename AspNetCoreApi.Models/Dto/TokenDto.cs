using System.ComponentModel.DataAnnotations;

namespace AspNetCoreApi.Models.Dto
{
    public class TokenDto
    {
        [Required]
        public string Token { get; set; }
    }
}