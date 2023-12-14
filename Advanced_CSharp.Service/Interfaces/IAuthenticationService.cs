using Advanced_CSharp.DTO.Requests.Authentication;
using Advanced_CSharp.DTO.Responses.Authentication;

namespace Advanced_CSharp.Service.Interfaces;


public interface IAuthenticationService

{
    /// <summary>
    /// RegisterUser
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    Task<AuthenticationRegisterResponse> RegisterUser(AuthenticationRegisterRequest Request);
    /// <summary>
    /// ValidateUser
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    Task<AuthenticationLoginResponse> ValidateUser(AuthenticationLoginRequest Request);

}
