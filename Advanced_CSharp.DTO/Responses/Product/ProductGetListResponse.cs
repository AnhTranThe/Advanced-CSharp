using Advanced_CSharp.Database.Commons;

namespace Advanced_CSharp.DTO.Responses.Product
{
    public class ProductGetListResponse
    {

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public double TotalProduct { get; set; }

        public int TotalPage { get; set; }

        public BaseResponse BaseResponse { get; set; } = new BaseResponse();

        public List<ProductResponse> productGetListResponses { get; set; } = new List<ProductResponse>();

    }
}
