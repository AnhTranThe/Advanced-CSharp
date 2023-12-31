﻿using Advanced_CSharp.Database.Enums;

namespace Advanced_CSharp.DTO.Requests.User
{
    public class UserUpdateRequest
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }

        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public EGender Gender { get; set; } = (EGender)(-1);
        public DateTimeOffset? Dob { get; set; }
        public string? Address { get; set; }


    }
}
