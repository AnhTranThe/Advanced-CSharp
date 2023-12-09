namespace Advanced_CSharp.DTO.Requests.CartDetail
{
    public class CartDetailUpdateItemRequest
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Unit { get; set; } = string.Empty;
        public string Images { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string Category { get; set; } = string.Empty;

    }
}
