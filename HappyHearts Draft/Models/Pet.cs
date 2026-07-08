namespace HappyHearts_Draft.Models
{
    public class Pet
    {
        public int PetID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Species { get; set; } = string.Empty;

        public string Breed { get; set; } = string.Empty;

        public int AgeInYears { get; set; }

        public string Gender { get; set; } = string.Empty;

        public string Condition { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public bool IsAvailable { get; set; }

        // Only used for fish
        public string? WaterType { get; set; }

        public bool IsFeatured { get; set; }
    }
}
