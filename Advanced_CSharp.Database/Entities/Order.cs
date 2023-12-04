using Advanced_CSharp.Database.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Advanced_CSharp.Database.Entities
{
    [Table("Orders")]
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Amount { get; set; } = 0;
        public EOrderStatus Status { get; set; }


    }
}
