using Advanced_CSharp.Database.Commons;

namespace Advanced_CSharp.DTO.Responses.CartDetail
{
    public class CartDetailUpdateItemResponse
    {
        public BaseResponse BaseResponse { get; set; } = new BaseResponse();
        public CartDetailResponse oldCartDetailResponse { get; set; } = new CartDetailResponse();
        public CartDetailResponse updatedCartDetailResponse { get; set; } = new CartDetailResponse();

    }
}
