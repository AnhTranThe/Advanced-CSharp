namespace Advanced_CSharp.DTO.Requests.Product
{
    public class ProductCheckRequest
    {
        public Guid ProductId { get; set; } = Guid.Empty;
        public string? ProductName { get; set; }
    }
}
