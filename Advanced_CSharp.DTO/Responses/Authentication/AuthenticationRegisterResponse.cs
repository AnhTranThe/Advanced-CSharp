using Advanced_CSharp.DTO.Responses.AppVersion;
using Advanced_CSharp.Database.Commons;

namespace Advanced_CSharp.DTO.Responses.Authentication
{
    public class AuthenticationRegisterResponse
    {

        public BaseResponse BaseResponse { get; set; } = new BaseResponse();

        public AppVersionResponse Data { get; set; } = new AppVersionResponse();


    }
}
