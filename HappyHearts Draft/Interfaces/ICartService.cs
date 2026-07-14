using HappyHearts_Draft.Models;

namespace HappyHearts_Draft.Interfaces
{
    public interface ICartService
    {
        Task<Cart?> GetActiveCartAsync(string userId);

        Task<Cart> CreateCartAsync(string userId);

        Task<List<CartDetails>> GetCartItemsAsync(long cartId);

        Task<bool> AddProductToCartAsync(
            string userId,
            long productId,
            int quantity);

        Task<bool> AddPetToCartAsync(
            string userId,
            long petId,
            int quantity);

        Task<bool> UpdateQuantityAsync(
            long cartDetailId,
            int quantity);

        Task<bool> RemoveItemAsync(
            long cartDetailId);

        Task<decimal> GetCartTotalAsync(
            long cartId);

        Task<int> GetCartCountAsync(
            string userId);

    }
}
