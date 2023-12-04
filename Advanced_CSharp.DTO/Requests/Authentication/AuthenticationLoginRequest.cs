using System.ComponentModel.DataAnnotations;

namespace Advanced_CSharp.DTO.Requests.Authentication
{
    public class AuthenticationLoginRequest
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;

    }
}
