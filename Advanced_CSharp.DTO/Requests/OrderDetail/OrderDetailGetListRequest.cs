using Advanced_CSharp.DTO.Pagination;

namespace Advanced_CSharp.DTO.Requests.OrderDetail
{
    public class OrderDetailGetListRequest : PaginationRequest
    {

        public Guid OrderId { get; set; }
        public new int PageSize { get; set; } = 10;
        public new int PageIndex { get; set; } = 1;
    }
}
