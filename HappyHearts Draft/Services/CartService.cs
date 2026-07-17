using HappyHearts_Draft.Interfaces;
using HappyHearts_Draft.Models;
using HappyHearts_Draft.Models.ViewModels;

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

                Console.WriteLine($"Pet object is null: {pet == null}");

                if (pet == null)
                {
                    throw new Exception($"Pet with ID {petId} was not found.");
                }

                var detail = new CartDetails
                {
                    CartId = cart.CartId,
                    PetId = petId,
                    Quantity = quantity,
                    Price = pet.Price
                };

                Console.WriteLine($"Cart null? {cart == null}");
                Console.WriteLine($"CartId = {cart?.CartId}");
                Console.WriteLine($"PetId = {petId}");
                Console.WriteLine($"Pet null? {pet == null}");

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


        public async Task<int> GetCartCountAsync(string userId)
        {
            var cart = await GetActiveCartAsync(userId);

            if (cart == null)
                return 0;

            var items = await GetCartItemsAsync(cart.CartId);

            return items.Sum(x => x.Quantity);
        }

        public async Task<List<CartItemViewModel>> GetCartViewAsync(string userId)
        {
            var cart = await GetActiveCartAsync(userId);

            if (cart == null)
                return new List<CartItemViewModel>();

            var details = await GetCartItemsAsync(cart.CartId);

            var list = new List<CartItemViewModel>();

            foreach (var item in details)
            {
                // ================= PRODUCTS =================
                if (item.ProductId.HasValue)
                {
                    var product = await _supabase.Client
                        .From<Product>()
                        .Where(x => x.ProductId == item.ProductId.Value)
                        .Single();

                    string imagePath = $"shop/{product.ImageUrl}";

                    list.Add(new CartItemViewModel
                    {
                        CartDetailId = item.CartDetailId,
                        ProductId = item.ProductId,
                        Name = product.Name,
                        ImageUrl = imagePath,
                        Quantity = item.Quantity,
                        Price = item.Price
                    });
                }

                // ================= PETS =================
                else if (item.PetId.HasValue)
                {
                    var pet = await _supabase.Client
                        .From<Pet>()
                        .Where(x => x.PetId == item.PetId.Value)
                        .Single();

                    string imagePath = pet.ImageUrl;

                    // Fish images don't have folders
                    if (!imagePath.Contains("/"))
                    {
                        imagePath = imagePath;
                    }

                    // Everything else already has its folder
                    // hamster/Hamster 1.webp
                    // bunnies/White Bunny.jpg

                    list.Add(new CartItemViewModel
                    {
                        CartDetailId = item.CartDetailId,
                        PetId = item.PetId,
                        Name = pet.Name,
                        ImageUrl = imagePath,
                        Quantity = item.Quantity,
                        Price = item.Price
                    });
                }
            }

            return list;
        }
    }
}
