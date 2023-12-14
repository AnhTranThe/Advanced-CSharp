namespace Advanced_CSharp.DTO.Requests.OrderDetail
{
    public class OrderDetailGetByIdRequest
    {
        public Guid OrderId { get; set; } = Guid.Empty;
        public Guid UserId { get; set; } = Guid.Empty;
    }
}
