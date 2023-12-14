using Advanced_CSharp.DTO.Responses.User;

namespace Advanced_CSharp.Service.Interfaces
{
    public interface IJwtService
    {

        Task<string> GenerateToken(UserResponse userResponse);

        Guid? ValidateJwtToken(string token);

    }
}
