using Advanced_CSharp.Database.Commons;

namespace Advanced_CSharp.DTO.Responses.OrderDetail
{
    public class OrderDetailAddItemResponse
    {
        public BaseResponse BaseResponse { get; set; } = new BaseResponse();
        public OrderDetailResponse orderDetailResponse { get; set; } = new OrderDetailResponse();
    }
}
