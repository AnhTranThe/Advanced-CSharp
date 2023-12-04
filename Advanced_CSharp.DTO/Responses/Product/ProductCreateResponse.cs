using Advanced_CSharp.Database.Commons;

namespace Advanced_CSharp.DTO.Responses.Product
{
    public class ProductCreateResponse
    {
        public BaseResponse BaseResponse { get; set; } = new BaseResponse();
        public ProductResponse productCreateResponse { get; set; } = new ProductResponse();

    }
}
