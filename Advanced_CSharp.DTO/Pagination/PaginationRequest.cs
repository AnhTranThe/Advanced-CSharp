namespace Advanced_CSharp.DTO.Pagination
{
    /// <summary>
    /// PaginationRequest
    /// </summary>
    public class PaginationRequest : IPaginationRequest
    {
        /// <summary>
        /// PageIndex
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// PageSize
        /// </summary>
        public int PageSize { get; set; }

        public PaginationRequest()
        {
            PageSize = 10;
            PageIndex = 1;
        }
        /// <summary>
        /// PaginationRequest
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public PaginationRequest(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
