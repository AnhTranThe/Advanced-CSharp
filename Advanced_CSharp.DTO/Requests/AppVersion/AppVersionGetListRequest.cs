using Advanced_CSharp.DTO.Pagination;

namespace Advanced_CSharp.DTO.Requests.AppVersion
{
    public class AppVersionGetListRequest : PaginationRequest
    {
        public new int PageSize { get; set; } = 10;

        /// <summary>
        /// Page Index
        /// </summary>
        public new int PageIndex { get; set; } = 1;

        /// <summary>
        /// Search by Version
        /// </summary>
        public string Version { get; set; } = string.Empty;
    }
}
