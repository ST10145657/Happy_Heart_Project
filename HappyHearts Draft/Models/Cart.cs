using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace HappyHearts_Draft.Models
{
    [Table("Cart")]
    public class Cart : BaseModel
    {
        [PrimaryKey("CartId")]
        public long CartId { get; set; }

        [Column("UserId")]
        public string UserId { get; set; }

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [Column("UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [Column("Status")]
        public string Status { get; set; } = "Active";
    }
}
