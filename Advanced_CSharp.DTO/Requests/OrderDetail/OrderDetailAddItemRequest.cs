namespace Advanced_CSharp.DTO.Requests.OrderDetail
{
    public class OrderDetailAddItemRequest
    {
        public Guid OrderId { get; set; }
        public Guid CartId { get; set; }


    }
}
