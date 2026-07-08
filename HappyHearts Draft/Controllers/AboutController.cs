using Microsoft.AspNetCore.Mvc;

namespace HappyHearts_Draft.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Team()
        {
            return View();
        }
    }
}
