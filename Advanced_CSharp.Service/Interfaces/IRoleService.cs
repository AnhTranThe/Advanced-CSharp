using Advanced_CSharp.DTO.Requests.Role;
using Advanced_CSharp.DTO.Responses.Role;

namespace Advanced_CSharp.Service.Interfaces
{
    public interface IRoleService
    {
        Task<RoleSearchResponse> SearchAsync(RoleSearchRequest request);
    }
}
