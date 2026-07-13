using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace HappyHearts_Draft.Models
{
    [Table("AdoptionRequests")]
    public class AdoptionRequest : BaseModel
    {
        [PrimaryKey("RequestId")]
        public long RequestId { get; set; }

        [Column("PetId")]
        public long PetId { get; set; }

        [Column("UserId")]
        public string UserId { get; set; }

        [Column("Status")]
        public string Status { get; set; }

        [Column("Message")]
        public string Message { get; set; }

        [Column("RequestDate")]
        public DateTime RequestDate { get; set; }
    }
}
