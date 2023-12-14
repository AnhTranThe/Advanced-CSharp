namespace Advanced_CSharp.DTO.Requests.Cart
{
    public class CartCheckRequest
    {
        public Guid UserId { get; set; } = Guid.Empty;
        public Guid CartId { get; set; } = Guid.Empty;
    }
}
