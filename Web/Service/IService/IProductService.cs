using Web.Models;

namespace Web.Service.IService
{
    public interface IProductService
    {
        Task<ResponseDto?> GetAllProductListAsync();

        Task<ResponseDto?> GetProductByIdAsync(int couponId);

        Task<ResponseDto?> CreateProductAsync(ProductDto coupon);

        Task<ResponseDto?> UpdateProductAsync(ProductDto coupon);

        Task<ResponseDto?> DeleteProductAsync(int couponId);
    }
}
