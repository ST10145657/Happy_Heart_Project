namespace HappyHearts_Draft.Models
{
    public class HomeViewModel
    {
        public List<Pet> FeaturedPets { get; set; } = new();

        public List<Species> Species { get; set; } = new();
    }
}
