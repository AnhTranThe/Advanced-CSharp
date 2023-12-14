using System.ComponentModel.DataAnnotations.Schema;

namespace Advanced_CSharp.Database.Entities
{
    [Table("CartDetails")]
    public class CartDetail
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public Cart? Cart { get; set; }
        public Product? Product { get; set; }
    }
}
