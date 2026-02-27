using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ElRawabi_Backend.Dtos.Users
{
    public class LoginRequestDto
    {
        [Required,EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, PasswordPropertyText]
        public string Password { get; set; } = string.Empty;
    }
}