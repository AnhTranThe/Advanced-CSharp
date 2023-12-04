using Advanced_CSharp.Database.Commons;

namespace Advanced_CSharp.DTO.Responses.Product
{
    public class ProductUpdateResponse
    {

        public BaseResponse BaseResponse { get; set; } = new BaseResponse();
        public ProductResponse OldProductResponse { get; set; } = new ProductResponse();
        public ProductResponse UpdatedProductResponse { get; set; } = new ProductResponse();

    }
}
