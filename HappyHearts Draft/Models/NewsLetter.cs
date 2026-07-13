using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace HappyHearts_Draft.Models
{
    [Table("Newsletter")]
    public class NewsLetter : BaseModel
    {
        [PrimaryKey("NewsletterId")]
        public long NewsletterId { get; set; }

        [Column("Email")]
        public string Email { get; set; }
    }
}
