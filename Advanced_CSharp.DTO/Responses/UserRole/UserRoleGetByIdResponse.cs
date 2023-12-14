using Advanced_CSharp.Database.Commons;

namespace Advanced_CSharp.DTO.Responses.UserRole
{
    public class UserRoleGetByIdResponse
    {

        public BaseResponse BaseResponse { get; set; } = new BaseResponse();
        public UserRoleResponse userRoleResponse { get; set; } = new UserRoleResponse();


    }
}
