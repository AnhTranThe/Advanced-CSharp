using Advanced_CSharp.Database.Commons;

namespace Advanced_CSharp.DTO.Responses.UserRole
{
    public class UserRoleCreateResponse
    {
        public Guid RoleId { get; set; }
        public string? RoleName { get; set; }
        public BaseResponse BaseResponse { get; set; } = new BaseResponse();
        public UserRoleResponse userRoleResponse { get; set; } = new UserRoleResponse();
    }
}
