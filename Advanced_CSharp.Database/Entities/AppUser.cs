using Advanced_CSharp.Database.Commons;
using Advanced_CSharp.Database.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Advanced_CSharp.Database.Entities
{
    [Table("AppUsers")]
    public class AppUser : BaseEntity
    {
        // user Authentication

        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        [JsonIgnore]
        public string PasswordHash { get; set; } = string.Empty;
        [JsonIgnore]
        public string? Token { get; set; }

        // user Information 

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public EGender Gender { get; set; } = EGender.NotDetect;
        public DateTimeOffset? Dob { get; set; }
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        private string _FullName = string.Empty;
        public string FullName
        {
            get => _FullName;
            set
            {
                _FullName = value;
                _FullName = LastName + " " + FirstName;
            }


        }

    }
}
