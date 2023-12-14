using Advanced_CSharp.DTO.Pagination;

namespace Advanced_CSharp.DTO.Requests.Order
{
    public class OrderGetListRequest : PaginationRequest
    {
        public new int PageSize { get; set; } = 10;
        public new int PageIndex { get; set; } = 1;
        public Guid UserId { get; set; } = Guid.Empty;
    }
}
