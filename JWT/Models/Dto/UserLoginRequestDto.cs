using System.ComponentModel.DataAnnotations;

namespace JWT.Models.Dto
{
    public class UserLoginRequestDto
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
