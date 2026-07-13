using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace HappyHearts_Draft.Models
{
    [Table("Users")]
    public class AppUser : BaseModel
    {
        [PrimaryKey("Id", false)]
        public long Id { get; set; }

        [Column("UserId")]
        public string UserId { get; set; } = "";

        [Column("FullName")]
        public string FullName { get; set; } = "";

        [Column("Email")]
        public string Email { get; set; } = "";

        [Column("Role")]
        public string Role { get; set; } = "Customer";

        [Column("Newsletter")]
        public bool Newsletter { get; set; }

        [Column("Created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
