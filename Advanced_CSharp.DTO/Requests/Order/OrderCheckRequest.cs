namespace Advanced_CSharp.DTO.Requests.Order
{
    public class OrderCheckRequest
    {
        public Guid UserId { get; set; } = Guid.Empty;
        public Guid OrderId { get; set; } = Guid.Empty;

    }
}
