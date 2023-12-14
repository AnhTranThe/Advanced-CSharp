namespace Advanced_CSharp.DTO.Requests.CartDetail
{
    public class CartDetailAddItemRequest
    {
        public Guid ProductId { get; set; } = Guid.Empty;
        public int Quantity { get; set; } = 0;
    }
}
