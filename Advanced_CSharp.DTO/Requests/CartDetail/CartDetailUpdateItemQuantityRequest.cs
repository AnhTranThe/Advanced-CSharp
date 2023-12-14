namespace Advanced_CSharp.DTO.Requests.CartDetail
{
    public class CartDetailUpdateItemQuantityRequest
    {

        public Guid ProductId { get; set; } = Guid.Empty;
        public int Quantity { get; set; }


    }
}
