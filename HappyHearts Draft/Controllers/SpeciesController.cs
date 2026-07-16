using HappyHearts_Draft.Interfaces;
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

        public async Task<IActionResult> Fish()
        {
            var pets = await _petService.GetPetsBySpeciesAsync(1);

            return View(pets);
        }

        public IActionResult Birds()
        {
            return View();
        }

        public async Task<IActionResult> Bunnies()
        {
            var pets = await _petService.GetPetsBySpeciesAsync(6);

            return View(pets);
        }
        public async Task<IActionResult> Hamsters()
        {
            var pets = await _petService.GetPetsBySpeciesAsync(5);

            return View(pets);
        }

        private readonly IPetService _petService;

        public SpeciesController(IPetService petService)
        {
            _petService = petService;
        }

        //public IActionResult Details(string type)
        //{
        //    ViewBag.SpeciesType = type;

        //    return View();
        //}
    }
}
