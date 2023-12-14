using Advanced_CSharp.Database.Commons;

namespace Advanced_CSharp.DTO.Responses.Order
{
    public class OrderGetListResponse
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public double TotalItems { get; set; }

        public int TotalPage { get; set; }

        public BaseResponse BaseResponse { get; set; } = new BaseResponse();

        public List<OrderResponse> orderResponses { get; set; } = new List<OrderResponse>();

    }
}
