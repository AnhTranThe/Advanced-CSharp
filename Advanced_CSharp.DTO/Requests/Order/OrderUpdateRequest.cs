using Advanced_CSharp.Database.Enums;

namespace Advanced_CSharp.DTO.Requests.Order
{
    public class OrderUpdateRequest
    {
        public Guid OrderId { get; set; }
        public EOrderStatus Status { get; set; }

    }
}
