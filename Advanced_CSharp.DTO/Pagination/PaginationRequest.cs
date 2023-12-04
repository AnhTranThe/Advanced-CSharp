namespace Advanced_CSharp.DTO.Pagination
{
    public class PaginationRequest : IPaginationRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public PaginationRequest()
        {
            PageSize = 10;
            PageIndex = 1;
        }
        public PaginationRequest(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
