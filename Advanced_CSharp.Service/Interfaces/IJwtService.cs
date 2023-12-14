using Advanced_CSharp.DTO.Responses.User;

namespace Advanced_CSharp.Service.Interfaces
{
    public interface IJwtService
    {

        /// <summary>
        /// GenerateToken
        /// </summary>
        /// <param name="userResponse"></param>
        /// <returns></returns>
        Task<string> GenerateToken(UserResponse userResponse);
        /// <summary>
        /// ValidateJwtToken
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>

        Guid? ValidateJwtToken(string token);

    }
}
