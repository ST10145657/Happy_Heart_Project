using HappyHearts_Draft.Interfaces;
using HappyHearts_Draft.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HappyHearts_Draft.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IProductService _productService;

        public AdminController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Dashboard()
        {
            var products = await _productService.GetProductsAsync();

            ViewBag.TotalProducts = products.Count;

            return View();
        }

        public async Task<IActionResult> Products()
        {
            var products = await _productService.GetProductsAsync();

            return View(products);
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            await _productService.AddProductAsync(product);

            return RedirectToAction(nameof(Products));
        }

        public async Task<IActionResult> EditProduct(long id)
        {
            var product = await _productService.GetProductAsync(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            await _productService.UpdateProductAsync(product);

            return RedirectToAction(nameof(Products));
        }

        public async Task<IActionResult> DeleteProduct(long id)
        {
            await _productService.DeleteProductAsync(id);

            return RedirectToAction(nameof(Products));
        }


    }
}
