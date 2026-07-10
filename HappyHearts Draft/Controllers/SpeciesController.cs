using Microsoft.AspNetCore.Mvc;

namespace HappyHearts_Draft.Controllers
{
    public class SpeciesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dogs()
        {
            return View();
        }

        public IActionResult Cats()
        {
            return View();
        }

        public IActionResult Fish()
        {
            return View();
        }

        public IActionResult Birds()
        {
            return View();
        }

        public IActionResult Bunnies()
        {
            return View();
        }

        //public IActionResult Details(string type)
        //{
        //    ViewBag.SpeciesType = type;

        //    return View();
        //}
    }
}
