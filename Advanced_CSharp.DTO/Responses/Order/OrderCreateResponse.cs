using Advanced_CSharp.Database.Commons;

namespace Advanced_CSharp.DTO.Responses.Order
{
    public class OrderCreateResponse
    {
        public BaseResponse BaseResponse { get; set; } = new BaseResponse();
        public OrderResponse OrderResponse { get; set; } = new OrderResponse();
    }
}
