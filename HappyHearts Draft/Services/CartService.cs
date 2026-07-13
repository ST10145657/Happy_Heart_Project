using HappyHearts_Draft.Interfaces;
using HappyHearts_Draft.Models;

namespace HappyHearts_Draft.Services
{
    public class CartService : ICartService
    {
        private readonly ISupabaseService _supabase;

        public CartService(ISupabaseService supabase)
        {
            _supabase = supabase;
        }

        public async Task<Cart?> GetActiveCartAsync(string userId)
        {
            var response = await _supabase.Client
        .From<Cart>()
        .Where(x => x.UserId == userId && x.Status == "Active")
        .Get();

            return response.Models.FirstOrDefault();
        }

        public async Task<Cart> CreateCartAsync(string userId)
        {
            var cart = new Cart
            {
                UserId = userId,
                Status = "Active",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var response = await _supabase.Client
                .From<Cart>()
                .Insert(cart);

            return response.Models.First();
        }

        public async Task<List<CartDetails>> GetCartItemsAsync(long cartId)
        {
            var response = await _supabase.Client
       .From<CartDetails>()
       .Where(x => x.CartId == cartId)
       .Get();

            return response.Models;
        }

        public async Task<bool> AddProductToCartAsync(
            string userId,
            long productId,
            int quantity)
        {
            // Find active cart
            var cart = await GetActiveCartAsync(userId);

            if (cart == null)
                cart = await CreateCartAsync(userId);

            // Check if product already exists in cart
            var existing = await _supabase.Client
                .From<CartDetails>()
                .Where(x => x.CartId == cart.CartId &&
                            x.ProductId == productId)
                .Get();

            if (existing.Models.Any())
            {
                var item = existing.Models.First();

                item.Quantity += quantity;

                await _supabase.Client
                    .From<CartDetails>()
                    .Update(item);
            }
            else
            {
                // Get product price
                var product = await _supabase.Client
                    .From<Product>()
                    .Where(x => x.ProductId == productId)
                    .Single();

                var detail = new CartDetails
                {
                    CartId = cart.CartId,
                    ProductId = productId,
                    Quantity = quantity,
                    Price = product.Price
                };

                await _supabase.Client
                    .From<CartDetails>()
                    .Insert(detail);
            }

            // Update cart timestamp
            cart.UpdatedAt = DateTime.UtcNow;

            await _supabase.Client
                .From<Cart>()
                .Update(cart);

            return true;
        }

        public async Task<bool> AddPetToCartAsync(
            string userId,
            long petId,
            int quantity)
        {
            var cart = await GetActiveCartAsync(userId);

            if (cart == null)
                cart = await CreateCartAsync(userId);

            var existing = await _supabase.Client
                .From<CartDetails>()
                .Where(x => x.CartId == cart.CartId &&
                            x.PetId == petId)
                .Get();

            if (existing.Models.Any())
            {
                var item = existing.Models.First();

                item.Quantity += quantity;

                await _supabase.Client
                    .From<CartDetails>()
                    .Update(item);
            }
            else
            {
                var pet = await _supabase.Client
                    .From<Pet>()
                    .Where(x => x.PetId == petId)
                    .Single();

                var detail = new CartDetails
                {
                    CartId = cart.CartId,
                    PetId = petId,
                    Quantity = quantity,
                    Price = pet.Price
                };

                await _supabase.Client
                    .From<CartDetails>()
                    .Insert(detail);
            }

            cart.UpdatedAt = DateTime.UtcNow;

            await _supabase.Client
                .From<Cart>()
                .Update(cart);

            return true;
        }

        public async Task<bool> UpdateQuantityAsync(long cartDetailId, int quantity)
        {
            var response = await _supabase.Client
                .From<CartDetails>()
                .Where(x => x.CartDetailId == cartDetailId)
                .Get();

            var item = response.Models.FirstOrDefault();

            if (item == null)
                return false;

            item.Quantity = quantity;

            await _supabase.Client
                .From<CartDetails>()
                .Update(item);

            return true;
        }

        public async Task<bool> RemoveItemAsync(long cartDetailId)
        {
            var response = await _supabase.Client
                .From<CartDetails>()
                .Where(x => x.CartDetailId == cartDetailId)
                .Get();

            var item = response.Models.FirstOrDefault();

            if (item == null)
                return false;

            await _supabase.Client
                .From<CartDetails>()
                .Delete(item);

            return true;
        }

        public async Task<decimal> GetCartTotalAsync(long cartId)
        {
            var items = await GetCartItemsAsync(cartId);

            decimal total = 0;

            foreach (var item in items)
            {
                total += item.Price * item.Quantity;
            }

            return total;
        }
    }
}
