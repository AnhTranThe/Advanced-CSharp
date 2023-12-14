using Advanced_CSharp.Database.Commons;

namespace Advanced_CSharp.DTO.Responses.OrderDetail
{
    public class OrderDetailSumTotalPriceResponse
    {
        public decimal totalPrice { get; set; }
        public BaseResponse baseResponse { get; set; } = new BaseResponse();

    }
}
