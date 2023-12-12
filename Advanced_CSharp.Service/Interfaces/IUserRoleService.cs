using Advanced_CSharp.DTO.Requests.UserRole;
using Advanced_CSharp.DTO.Responses.UserRole;

namespace Advanced_CSharp.Service.Interfaces
{
    public interface IUserRoleService
    {
        Task<UserRoleCreateResponse> AddUserRoleAsync(UserRoleCreateRequest request);

        Task<UserRoleGetByIdResponse> GetByIdAsync(UserRoleGetByIdRequest request);

        Task<UserRoleGetByIdResponse> GetByIdRegisterAsync(UserRoleGetByIdRequest request);


    }
}
