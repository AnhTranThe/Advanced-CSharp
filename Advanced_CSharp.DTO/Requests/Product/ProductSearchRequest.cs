namespace Advanced_CSharp.DTO.Requests.Product
{
    public class ProductSearchRequest
    {
        public string Name { get; set; } = string.Empty;
        public int PageSize { get; set; } = 5;
        public int PageNumber { get; set; } = 1;

    }
}
