using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace HappyHearts_Draft.Models
{
    [Table("CartDetails")]
    public class CartDetails: BaseModel
    {
        [PrimaryKey("CartDetailId", false)]
        public long CartDetailId { get; set; }

        [Column("CartId")]
        public long CartId { get; set; }

        [Column("ProductId")]
        public long? ProductId { get; set; }

        [Column("PetId")]
        public long? PetId { get; set; }

        [Column("Quantity")]
        public int Quantity { get; set; }

        [Column("Price")]
        public decimal Price { get; set; }
    }
}
