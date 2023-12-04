using Advanced_CSharp.DTO.Requests.Authentication;
using Advanced_CSharp.DTO.Responses.Authentication;

namespace Advanced_CSharp.Service.Interfaces;

public interface IAuthenticationService
{
    Task<AuthenticationRegisterResponse> RegisterUser(AuthenticationRegisterRequest Request);
    Task<AuthenticationLoginResponse> ValidateUser(AuthenticationLoginRequest Request);

}
