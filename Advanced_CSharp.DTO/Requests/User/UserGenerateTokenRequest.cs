namespace Advanced_CSharp.DTO.Requests.User
{
    public class UserGenerateTokenRequest
    {
        public Guid UserId { get; set; } = Guid.Empty;
        public string? Token { get; set; }
    }
}
