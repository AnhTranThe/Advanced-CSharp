using Advanced_CSharp.Database.Commons;

namespace Advanced_CSharp.DTO.Responses.User
{
    public class UserUpdateResponse
    {
        public BaseResponse BaseResponse { get; set; } = new BaseResponse();
        public UserResponse OldUserResponse { get; set; } = new UserResponse();
        public UserResponse UpdatedUserResponse { get; set; } = new UserResponse();
    }
}
