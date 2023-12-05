using System.ComponentModel.DataAnnotations.Schema;

namespace Advanced_CSharp.Database.Entities
{
    [Table("AppRoles")]
    public class AppRole
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string RoleName { get; set; } = string.Empty;

    }
}
