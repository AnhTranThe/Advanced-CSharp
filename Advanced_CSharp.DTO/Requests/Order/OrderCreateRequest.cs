using Advanced_CSharp.Database.Enums;

namespace Advanced_CSharp.DTO.Requests.Order
{
    public class OrderCreateRequest
    {
        public Guid UserId { get; set; } = Guid.Empty;
        public Guid OrderId { get; set; } = Guid.Empty;
        public EOrderStatus OrderStatus { get; set; } = EOrderStatus.Processing;

    }
}
