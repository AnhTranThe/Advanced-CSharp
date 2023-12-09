using Advanced_CSharp.Database.Commons;

namespace Advanced_CSharp.DTO.Responses.CartDetail
{
    public class CartDetailDeleteResponse
    {
        public BaseResponse BaseResponse { get; set; } = new BaseResponse();

        public CartDetailResponse cartDetailResponse { get; set; } = new CartDetailResponse();
    }
}
