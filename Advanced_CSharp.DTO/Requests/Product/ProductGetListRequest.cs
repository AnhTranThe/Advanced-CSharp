using Advanced_CSharp.DTO.Pagination;

namespace Advanced_CSharp.DTO.Requests.Product
{
    public class ProductGetListRequest : PaginationRequest
    {
        public new int PageSize { get; set; } = 10;

        public new int PageIndex { get; set; } = 1;



    }
}
