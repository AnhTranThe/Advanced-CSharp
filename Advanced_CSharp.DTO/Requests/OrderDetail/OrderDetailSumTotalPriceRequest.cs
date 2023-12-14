namespace Advanced_CSharp.DTO.Requests.OrderDetail
{
    public class OrderDetailSumTotalPriceRequest
    {
        public Guid OrderId { get; set; } = Guid.Empty;
        public int Quantity { get; set; }
        public decimal CurrentPrice { get; set; }

    }
}
