using Advanced_CSharp.DTO.Requests.User;
using Advanced_CSharp.DTO.Responses.User;

namespace Advanced_CSharp.Service.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserGetByIdResponse> GetByIdAsync(UserGetByIdRequest request);
        /// <summary>
        /// SearchAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        Task<UserSearchResponse> SearchAsync(UserSearchRequest request);
        /// <summary>
        /// AddAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        Task<UserCreateResponse> AddAsync(UserCreateRequest request);
        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        Task<UserUpdateResponse> UpdateAsync(UserUpdateRequest request);
        /// <summary>
        /// GenerateTokenAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        Task<UserGenerateTokenResponse> GenerateTokenAsync(UserGenerateTokenRequest request);

    }
}
