using Microsoft.AspNetCore.Mvc;

namespace HappyHearts_Draft.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Pets()
        {
            return View();
        }

        public IActionResult AddPet()
        {
            return View();
        }

        public IActionResult EditPet(int id)
        {
            return View();
        }


    }
}
