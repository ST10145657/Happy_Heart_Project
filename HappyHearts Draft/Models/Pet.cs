using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace HappyHearts_Draft.Models
{
    [Table("Pets")]
    public class Pet : BaseModel
    {
        [PrimaryKey("PetId", false)]
        public long PetId { get; set; }

        [Column("Name")]
        public string Name { get; set; } = string.Empty;

        [Column("SpeciesId")]
        public long SpeciesId { get; set; }

        [Column("Breed")]
        public string Breed { get; set; } = string.Empty;

        [Column("Gender")]
        public string Gender { get; set; } = string.Empty;

        [Column("Age")]
        public int Age { get; set; }

        [Column("Price")]
        public decimal Price { get; set; }

        [Column("Description")]
        public string Description { get; set; } = string.Empty;

        [Column("ImageUrl")]
        public string ImageUrl { get; set; } = string.Empty;

        [Column("Colour")]
        public string? Colour { get; set; }

        [Column("Size")]
        public string? Size { get; set; }

        [Column("Temperament")]
        public string? Temperament { get; set; }

        [Column("Temperature")]
        public string? Temperature { get; set; }

        [Column("Lifespan")]
        public string? Lifespan { get; set; }

        [Column("Difficulty")]
        public string? Difficulty { get; set; }

        // Only used for fish
        [Column("Type")]
        public string? Type { get; set; }

        [Column("IsAvailable")]
        public bool IsAvailable { get; set; }

        [Column("IsFeatured")]
        public bool IsFeatured { get; set; }
    }
}
