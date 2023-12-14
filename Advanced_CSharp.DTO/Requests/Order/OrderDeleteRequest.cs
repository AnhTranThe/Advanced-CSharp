namespace Advanced_CSharp.DTO.Requests.Order
{
    public class OrderDeleteRequest
    {
        public Guid OrderId { get; set; } = Guid.Empty;
        public Guid UserId { get; set; } = Guid.Empty;
    }
}
