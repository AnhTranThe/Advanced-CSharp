using Advanced_CSharp.Database.Commons;

namespace Advanced_CSharp.DTO.Responses.CartDetail
{
    public class CartDetailGetListResponse
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public double TotalProduct { get; set; }

        public int TotalPage { get; set; }
        public decimal TotalAmount { get; set; }
        public BaseResponse BaseResponse { get; set; } = new BaseResponse();
        public List<CartDetailResponse> cartDetailResponses { get; set; } = new List<CartDetailResponse>();
    }
}
