using Advanced_CSharp.Database.Commons;

namespace Advanced_CSharp.DTO.Responses.OrderDetail
{
    public class OrderDetailGetByIdResponse
    {
        public BaseResponse baseResponse { get; set; } = new BaseResponse();
        public OrderDetailResponse orderDetailResponse { get; set; } = new OrderDetailResponse();
    }
}
