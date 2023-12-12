namespace Advanced_CSharp.DTO.Requests.Product
{
    public class ProductUpdateRequest
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public int Inventory { get; set; } = 0;
        public string Unit { get; set; } = "VND";
        public string Images { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;


    }
}
