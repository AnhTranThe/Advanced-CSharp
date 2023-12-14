namespace Advanced_CSharp.DTO.Requests.Product
{
    public class ProductUpdateInventoryRequest
    {
        public Guid Id { get; set; }
        public int Inventory { get; set; } = 0;
    }
}
