using Advanced_CSharp.Database.Commons;

namespace Advanced_CSharp.DTO.Responses.Role
{
    public class RoleSearchResponse
    {
        public BaseResponse BaseResponse { get; set; } = new BaseResponse();

        public RoleResponse roleResponse { get; set; } = new RoleResponse();
    }
}
