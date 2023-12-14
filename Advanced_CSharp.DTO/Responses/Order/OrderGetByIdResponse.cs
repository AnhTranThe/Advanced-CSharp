using Advanced_CSharp.Database.Commons;

namespace Advanced_CSharp.DTO.Responses.Order
{
    public class OrderGetByIdResponse
    {
        public BaseResponse BaseResponse { get; set; } = new BaseResponse();

        public OrderResponse orderResponse { get; set; } = new OrderResponse();
    }
}
