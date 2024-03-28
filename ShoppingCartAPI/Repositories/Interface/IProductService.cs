using ShoppingCartAPI.Common;
using ShoppingCartAPI.Entities.Dto;

namespace ShoppingCartAPI.Repositories.Interface
{
    public interface IProductService
    {
        Task<List<ProductDto>> ProductList(string ProductId);
    }
}
