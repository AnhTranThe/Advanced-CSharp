using Advanced_CSharp.Database.Commons;

namespace Advanced_CSharp.DTO.Responses.CartDetail
{
    public class CartDetailGetItemByIdResponse
    {
        public BaseResponse BaseResponse { get; set; } = new BaseResponse();

        public CartDetailResponse cartDetailResponses { get; set; } = new CartDetailResponse();
    }
}
