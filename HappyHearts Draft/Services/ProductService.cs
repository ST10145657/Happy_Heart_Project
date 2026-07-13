using HappyHearts_Draft.Interfaces;
using HappyHearts_Draft.Models;

namespace HappyHearts_Draft.Services
{
    public class ProductService
    {
        private readonly ISupabaseService _supabase;

        public ProductService(ISupabaseService supabase)
        {
            _supabase = supabase;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var response = await _supabase.Client
                .From<Product>()
                .Get();

            return response.Models;
        }

        public async Task<Product?> GetProductAsync(long productId)
        {
            var response = await _supabase.Client
                .From<Product>()
                .Where(x => x.ProductId == productId)
                .Get();

            return response.Models.FirstOrDefault();
        }
    }
}
