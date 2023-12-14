using Advanced_CSharp.DTO.Requests.Role;
using Advanced_CSharp.DTO.Responses.Role;

namespace Advanced_CSharp.Service.Interfaces
{
    public interface IRoleService
    {
        /// <summary>
        /// SearchAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<RoleSearchResponse> SearchAsync(RoleSearchRequest request);
        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<RoleGetByIdResponse> GetByIdAsync(RoleGetByIdRequest request);
    }
}
