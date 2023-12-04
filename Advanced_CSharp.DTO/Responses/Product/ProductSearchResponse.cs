using Advanced_CSharp.Database.Commons;

namespace Advanced_CSharp.DTO.Responses.Product
{
    public class ProductSearchResponse
    {

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public long Total { get; set; }

        public BaseResponse BaseResponse { get; set; } = new BaseResponse();

        public List<ProductResponse> productSearchResponses { get; set; } = new List<ProductResponse>();

    }
}
