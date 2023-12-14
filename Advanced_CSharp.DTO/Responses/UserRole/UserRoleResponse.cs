namespace Advanced_CSharp.DTO.Responses.UserRole
{
    public class UserRoleResponse
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public Guid RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }
}
