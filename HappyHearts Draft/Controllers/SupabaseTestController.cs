using HappyHearts_Draft.Interfaces;
using HappyHearts_Draft.Models;
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

        public async Task< IActionResult> Index()
        {
            var products = await _supabase.Client
          .From<Product>()
          .Get();

            return Content($"✅ Connected! Found {products.Models.Count} products.");
        }
    }
}
