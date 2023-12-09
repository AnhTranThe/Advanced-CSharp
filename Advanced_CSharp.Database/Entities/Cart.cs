using Advanced_CSharp.Database.Commons;
using System.ComponentModel.DataAnnotations.Schema;

namespace Advanced_CSharp.Database.Entities
{
    [Table("Carts")]
    public class Cart : BaseEntity
    {
        public Guid UserId { get; set; }


    }
}
