
using ProductAPI.Entities;

namespace ProductAPI.Repositories.Interface
{
    public interface IProductRepository
    {
        Task<List<ProductModel?>> GetProductList();

        Task<ProductModel?> GetProductById(int productId);

        Task<ProductModel?> CreateProduct(ProductModel product);

        Task<ProductModel?> UpdateProduct(ProductModel product);

        Task<bool> DeleteProduct(int productId);
    }
}
