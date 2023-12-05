﻿using Advanced_CSharp.Database.Commons;

namespace Advanced_CSharp.DTO.Responses.User
{
    public class UserSearchResponse
    {
        public BaseResponse BaseResponse { get; set; } = new BaseResponse();

        public UserResponse userResponse { get; set; } = new UserResponse();
    }
}
