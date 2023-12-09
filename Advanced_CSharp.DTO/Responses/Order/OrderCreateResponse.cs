using Advanced_CSharp.Database.Commons;

namespace Advanced_CSharp.DTO.Responses.Order
{
    public class OrderCreateResponse
    {
        private BaseResponse BaseResponse { get; set; } = new BaseResponse();
        private OrderResponse OrderResponse { get; set; } = new OrderResponse();
    }
}
