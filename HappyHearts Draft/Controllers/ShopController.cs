using HappyHearts_Draft.Interfaces;
using HappyHearts_Draft.Models;
using HappyHearts_Draft.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using HappyHearts_Draft.Services;

namespace HappyHearts_Draft.Controllers
{
    public class ShopController : Controller
    {
        private readonly ProductService _productService;

        public ShopController(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProductsAsync();

            return View(products);
        }
    }
}
