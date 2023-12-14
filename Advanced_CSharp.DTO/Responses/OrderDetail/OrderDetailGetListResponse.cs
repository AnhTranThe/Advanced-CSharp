using Advanced_CSharp.Database.Commons;

namespace Advanced_CSharp.DTO.Responses.OrderDetail
{
    public class OrderDetailGetListResponse
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public double TotalItems { get; set; }

        public int TotalPage { get; set; }
        public decimal TotalAmount { get; set; }

        public BaseResponse baseResponse { get; set; } = new BaseResponse();

        public List<OrderDetailResponse> orderDetailResponses { get; set; } = new List<OrderDetailResponse>();
    }
}
