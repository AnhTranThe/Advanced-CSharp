using Advanced_CSharp.Database.Enums;

namespace Advanced_CSharp.DTO.Responses.User
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public EGender Gender { get; set; }
        public DateTimeOffset? Dob { get; set; }
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTimeOffset? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public string PasswordHash { get; set; } = string.Empty;

        public string? Token { get; set; }
    }
}
