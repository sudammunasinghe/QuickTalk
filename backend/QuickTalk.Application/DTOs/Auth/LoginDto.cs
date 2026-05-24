using System.ComponentModel.DataAnnotations;

namespace QuickTalk.Application.DTOs.Auth
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
