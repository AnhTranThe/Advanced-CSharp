using Advanced_CSharp.Database.Commons;
using Advanced_CSharp.DTO.Responses.Order;

namespace Advanced_CSharp.DTO.Responses.OrderDetail
{
    public class OrderDetailGetByIdResponse
    {
        public OrderResponse orderResponse { get; set; } = new OrderResponse();
        public BaseResponse baseResponse { get; set; } = new BaseResponse();
        public List<OrderDetailResponse> orderDetailResponses { get; set; } = new List<OrderDetailResponse>();
    }
}
