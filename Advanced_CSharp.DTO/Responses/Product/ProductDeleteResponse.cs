using Advanced_CSharp.Database.Commons;

namespace Advanced_CSharp.DTO.Responses.Product
{
    public class ProductDeleteResponse
    {
        public BaseResponse BaseResponse { get; set; } = new BaseResponse();

        public ProductResponse productDeleteResponse { get; set; } = new ProductResponse();

    }
}
