using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Advanced_CSharp.Database.Entities
{
    [Table("AppRoles")]
    public class AppRole : IdentityRole
    {
        public string RoleName { get; set; } = string.Empty;

    }
}
