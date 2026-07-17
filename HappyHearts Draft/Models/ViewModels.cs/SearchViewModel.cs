using HappyHearts_Draft.Models;

namespace HappyHearts_Draft.Models.ViewModels
{
    public class SearchViewModel
    {
        public string SearchText { get; set; }

        public List<Product> Products { get; set; } = new();

        public List<Pet> Pets { get; set; } = new();
    }
}
