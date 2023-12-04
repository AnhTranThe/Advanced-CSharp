using Advanced_CSharp.Database.Commons;

namespace Advanced_CSharp.DTO.Responses.Product
{
    public class ProductGetByIdResponse
    {

        public BaseResponse BaseResponse { get; set; } = new BaseResponse();

        public ProductResponse productGetByIdResponse { get; set; } = new ProductResponse();

    }
}
