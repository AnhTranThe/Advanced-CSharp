using Advanced_CSharp.Database.Commons;

namespace Advanced_CSharp.DTO.Responses.CartDetail
{
    public class CartDetailSumTotalPriceResponse
    {
        public decimal totalPrice { get; set; }
        public BaseResponse baseResponse { get; set; } = new BaseResponse();

    }
}
