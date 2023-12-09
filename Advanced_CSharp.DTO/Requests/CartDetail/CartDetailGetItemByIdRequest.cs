namespace Advanced_CSharp.DTO.Requests.CartDetail
{
    public class CartDetailGetItemByIdRequest
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
    }
}
