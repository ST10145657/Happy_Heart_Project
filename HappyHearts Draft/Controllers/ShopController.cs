using Microsoft.AspNetCore.Mvc;

namespace HappyHearts_Draft.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
