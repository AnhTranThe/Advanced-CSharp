using Advanced_CSharp.Database.Commons;
using Advanced_CSharp.Database.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Advanced_CSharp.Database.Entities
{
    [Table("Orders")]
    public class Order : BaseEntity
    {

        public Guid UserId { get; set; }
        public decimal Amount { get; set; } = 0;
        public EOrderStatus Status { get; set; }
        public bool IsDeleted { get; set; } = false;

        public List<OrderDetail>? OrderDetails { get; set; }


    }
}
