using HappyHearts_Draft.Interfaces;
using HappyHearts_Draft.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HappyHearts_Draft.Controllers
{
    public class SearchController : Controller
    {
        private readonly IProductService _productService;
        private readonly IPetService _petService;

        public SearchController(
            IProductService productService,
            IPetService petService)
        {
            _productService = productService;
            _petService = petService;
        }

        public async Task<IActionResult> Index(string q)
        {
            var products = await _productService.GetProductsAsync();
            var pets = await _petService.GetAllPetsAsync();

            var model = new SearchViewModel
            {
                SearchText = q,

                Products = products
                    .Where(x =>
                        string.IsNullOrEmpty(q) ||
                        x.Name.Contains(q, StringComparison.OrdinalIgnoreCase))
                    .ToList(),

                Pets = pets
                    .Where(x =>
                        string.IsNullOrEmpty(q) ||
                        x.Name.Contains(q, StringComparison.OrdinalIgnoreCase))
                    .ToList()
            };

            return View(model);
        }
    }
}
