using Advanced_CSharp.Database.Commons;
using System.ComponentModel.DataAnnotations.Schema;

namespace Advanced_CSharp.Database.Entities
{
    [Table("Products")]
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Inventory { get; set; }
        public string Unit { get; set; } = string.Empty;
        public string Images { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }
}
