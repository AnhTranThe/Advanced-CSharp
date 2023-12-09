namespace Advanced_CSharp.DTO.Requests.CartDetail
{
    public class CartDetailAddItemRequest
    {

        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; } = 0;
    }
}
