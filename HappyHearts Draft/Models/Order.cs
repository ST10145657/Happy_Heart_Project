using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace HappyHearts_Draft.Models
{
    [Table("Orders")]
    public class Order : BaseModel
    {
        [PrimaryKey("OrderId")]
        public long OrderId { get; set; }

        [Column("UserId")]
        public string UserId { get; set; }

        [Column("Total")]
        public decimal Total { get; set; }

        [Column("Status")]
        public string Status { get; set; }

        [Column("OrderDate")]
        public DateTime OrderDate { get; set; }
    }
}
