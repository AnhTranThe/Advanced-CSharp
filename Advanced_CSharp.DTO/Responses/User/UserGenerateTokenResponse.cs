using Advanced_CSharp.Database.Commons;

namespace Advanced_CSharp.DTO.Responses.User
{
    public class UserGenerateTokenResponse
    {
        public string Token { get; set; } = string.Empty;
        public BaseResponse BaseResponse { get; set; } = new BaseResponse();


    }
}
