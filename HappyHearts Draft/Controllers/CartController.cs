using HappyHearts_Draft.Services;
using Microsoft.AspNetCore.Mvc;
using HappyHearts_Draft.Interfaces;
using System.Security.Claims;

namespace HappyHearts_Draft.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(long productId, int quantity)
        {
            if (!User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            await _cartService.AddProductToCartAsync(
                userId,
                productId,
                quantity);

            TempData["Success"] = "Item added to cart!";

            return Redirect(Request.Headers["Referer"].ToString());
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
