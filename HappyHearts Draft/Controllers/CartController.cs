using HappyHearts_Draft.Services;
using Microsoft.AspNetCore.Mvc;
using HappyHearts_Draft.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace HappyHearts_Draft.Controllers
{
    [Authorize(Roles = "Customer,Admin")]
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

        [HttpPost]
        public async Task<IActionResult> AddPet(long petId, int quantity)
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

            await _cartService.AddPetToCartAsync(
                userId,
                petId,
                quantity);

            TempData["Success"] = "Pet added to cart!";

            return Redirect(Request.Headers["Referer"].ToString());
        }
        public async Task<IActionResult> Index()
        {
            if (!User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var items = await _cartService.GetCartViewAsync(userId!);

            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(long cartDetailId, int quantity)
        {
            await _cartService.UpdateQuantityAsync(cartDetailId, quantity);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItem(long cartDetailId)
        {
            await _cartService.RemoveItemAsync(cartDetailId);

            return RedirectToAction(nameof(Index));
        }
    }
}
