using Advanced_CSharp.Database.Enums;

namespace Advanced_CSharp.DTO.Requests.Order
{
    public class OrderCreateRequest
    {
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
        public EOrderStatus OrderStatus { get; set; } = EOrderStatus.Processing;

    }
}
