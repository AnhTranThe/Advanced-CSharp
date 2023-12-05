using Advanced_CSharp.DTO.Requests.User;
using Advanced_CSharp.DTO.Responses.User;

namespace Advanced_CSharp.Service.Interfaces
{
    public interface IUserService
    {
        Task<UserGetByIdResponse> GetByIdAsync(UserGetByIdRequest request);

        Task<UserSearchResponse> SearchAsync(UserSearchRequest request);

        Task<UserCreateResponse> AddAsync(UserCreateRequest request);

        Task<UserUpdateResponse> UpdateAsync(UserUpdateRequest request);

        Task<UserGenerateTokenResponse> GenerateTokenAsync(UserGenerateTokenRequest request);

    }
}
