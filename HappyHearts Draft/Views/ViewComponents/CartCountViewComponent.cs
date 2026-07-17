using HappyHearts_Draft.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HappyHearts_Draft.View.ViewComponents
{
    public class CartCountViewComponent : ViewComponent
    {
        private readonly ICartService _cartService;

        public CartCountViewComponent(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (!User.Identity!.IsAuthenticated)
                return View(0);

            var userId = UserClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return View(0);

            var count = await _cartService.GetCartCountAsync(userId);

            return View(count);
        }
    }
}
