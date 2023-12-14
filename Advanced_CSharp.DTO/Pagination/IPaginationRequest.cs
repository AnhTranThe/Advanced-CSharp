namespace Advanced_CSharp.DTO.Pagination
{
    /// <summary>
    /// IPaginationRequest
    /// </summary>
    public interface IPaginationRequest
    {
        /// <summary>
        /// PageIndex
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// PageSize
        /// </summary>
        public int PageSize { get; set; }

    }
}
