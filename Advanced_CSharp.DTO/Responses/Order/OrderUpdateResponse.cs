using Advanced_CSharp.Database.Commons;
using Advanced_CSharp.Database.Enums;

namespace Advanced_CSharp.DTO.Responses.Order
{
    public class OrderUpdateResponse
    {
        public BaseResponse BaseResponse { get; set; } = new BaseResponse();
        public EOrderStatus oldOrderStatus { get; set; } = new EOrderStatus();
        public EOrderStatus UpdatedOrderStatus { get; set; } = new EOrderStatus();
    }
}
