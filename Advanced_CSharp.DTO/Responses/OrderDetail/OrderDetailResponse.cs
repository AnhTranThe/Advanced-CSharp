namespace Advanced_CSharp.DTO.Responses.OrderDetail
{
    public class OrderDetailResponse
    {

        public Guid? Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal CurrentPrice { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public string Images { get; set; } = string.Empty;

    }
}
