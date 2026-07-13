using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace HappyHearts_Draft.Models
{
    [Table("Product")]
    public class Product : BaseModel
    {
        [PrimaryKey("ProductId", false)]
        public long ProductId { get; set; }

        [Column("Name")]
        public string Name { get; set; } = string.Empty;

        [Column("Description")]
        public string Description { get; set; } = string.Empty;

        [Column("Price")]
        public decimal Price { get; set; }

        [Column("ImageUrl")]
        public string ImageUrl { get; set; } = string.Empty;

        [Column("Category")]
        public string Category { get; set; } = string.Empty;

        [Column("Stock")]
        public int Stock { get; set; }
    }
}
