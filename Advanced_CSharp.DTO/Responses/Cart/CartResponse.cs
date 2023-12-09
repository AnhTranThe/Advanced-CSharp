namespace Advanced_CSharp.DTO.Responses.Cart
{
    public class CartResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; } = 0;


    }
}
