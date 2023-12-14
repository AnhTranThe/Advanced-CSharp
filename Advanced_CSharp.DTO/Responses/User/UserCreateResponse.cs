using Advanced_CSharp.Database.Commons;

namespace Advanced_CSharp.DTO.Responses.User
{
    public class UserCreateResponse
    {
        public BaseResponse BaseResponse { get; set; } = new BaseResponse();
        public UserResponse UserResponse { get; set; } = new UserResponse();

    }
}
