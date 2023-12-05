using System.ComponentModel.DataAnnotations;

namespace Advanced_CSharp.DTO.Requests.Authentication
{
    public class AuthenticationLoginRequest
    {

        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;

    }
}
