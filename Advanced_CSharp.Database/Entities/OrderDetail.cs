using System.ComponentModel.DataAnnotations.Schema;

namespace Advanced_CSharp.Database.Entities
{
    [Table("OrderDetails")]
    public class OrderDetail
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal CurrentPrice { get; set; }
        public Order? Order { get; set; }
        public Product? Product { get; set; }
    }
}
