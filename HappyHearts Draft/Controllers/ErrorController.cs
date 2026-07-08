using Microsoft.AspNetCore.Mvc;

namespace HappyHearts_Draft.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
