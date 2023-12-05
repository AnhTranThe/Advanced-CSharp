using Advanced_CSharp.Database.Commons;

namespace Advanced_CSharp.DTO.Responses.Authentication
{
    public class AuthenticationLoginResponse
    {

        public BaseResponse BaseResponse { get; set; } = new BaseResponse();

        public string Token { get; set; } = string.Empty;

    }
}
