using HappyHearts_Draft.Models;

namespace HappyHearts_Draft.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync();


        Task<Product?> GetProductAsync(long productId);

        Task<Product> AddProductAsync(Product product);

        Task<Product> UpdateProductAsync(Product product);

        Task<bool> DeleteProductAsync(long productId);
    }
}
