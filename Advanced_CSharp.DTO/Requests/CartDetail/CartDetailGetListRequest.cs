using Advanced_CSharp.DTO.Pagination;

namespace Advanced_CSharp.DTO.Requests.CartDetail
{
    public class CartDetailGetListRequest : PaginationRequest
    {
        public Guid CartId { get; set; }
        public new int PageSize { get; set; } = 10;
        public new int PageIndex { get; set; } = 1;
    }
}
