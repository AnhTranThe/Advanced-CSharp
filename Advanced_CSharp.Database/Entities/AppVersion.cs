using Advanced_CSharp.Database.Commons;
using System.ComponentModel.DataAnnotations.Schema;

namespace Advanced_CSharp.Database.Entities
{

    [Table("AppVersions")]
    public class AppVersion : BaseEntity
    {
        public string Version { get; set; } = string.Empty;
    }


}
