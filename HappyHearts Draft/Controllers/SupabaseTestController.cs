using HappyHearts_Draft.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HappyHearts_Draft.Controllers
{
    public class SupabaseTestController : Controller
    {
        private readonly ISupabaseService _supabase;

        public SupabaseTestController(ISupabaseService supabase)
        {
            _supabase = supabase;
        }

        public IActionResult Index()
        {
            return Content("✅ Connected to Supabase successfully!");
        }
    }
}
