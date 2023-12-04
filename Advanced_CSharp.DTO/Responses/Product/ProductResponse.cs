namespace Advanced_CSharp.DTO.Responses.Product
{
    public class ProductResponse
    {

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Inventory { get; set; }
        public string Unit { get; set; } = string.Empty;
        public string Images { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public DateTimeOffset? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

    }
}
