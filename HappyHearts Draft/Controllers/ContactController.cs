using Microsoft.AspNetCore.Mvc;

namespace HappyHearts_Draft.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
