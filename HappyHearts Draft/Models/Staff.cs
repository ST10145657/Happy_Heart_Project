using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace HappyHearts_Draft.Models
{
    public class Staff : BaseModel
    {
        [PrimaryKey("StaffId")]
        public long StaffId { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Role")]
        public string Role { get; set; }

        [Column("Email")]
        public string Email { get; set; }
    }
}
