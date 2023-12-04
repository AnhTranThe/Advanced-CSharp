using System.ComponentModel.DataAnnotations.Schema;

namespace Advanced_CSharp.Database.Entities
{
    [Table("AppUserRoles")]

    public class AppUserRole
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

    }
}
