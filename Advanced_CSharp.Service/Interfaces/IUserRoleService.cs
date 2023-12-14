using Advanced_CSharp.DTO.Requests.UserRole;
using Advanced_CSharp.DTO.Responses.UserRole;

namespace Advanced_CSharp.Service.Interfaces
{
    public interface IUserRoleService
    {
        /// <summary>
        /// AddUserRoleAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserRoleCreateResponse> AddUserRoleAsync(UserRoleCreateRequest request);
        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserRoleGetByIdResponse> GetByIdAsync(UserRoleGetByIdRequest request);
        /// <summary>
        /// GetByIdRegisterAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<UserRoleGetByIdResponse> GetByIdRegisterAsync(UserRoleGetByIdRequest request);


    }
}
