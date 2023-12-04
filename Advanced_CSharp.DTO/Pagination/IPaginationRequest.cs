namespace Advanced_CSharp.DTO.Pagination
{
    public interface IPaginationRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

    }
}
