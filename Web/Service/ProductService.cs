using Web.Models;
using Web.Service.IService;
using Web.Utility;

namespace Web.Service
{
    public class ProductService : IProductService
    {
        private readonly IBaseService _baseService;

        public ProductService(IBaseService baseService) 
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto?> CreateProductAsync(ProductDto product)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetail.MethodEnum.POST,
                Data = product,
                Url = StaticDetail.ProductApiBase + "/api/product"
            });
        }

        public async Task<ResponseDto?> DeleteProductAsync(int productId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetail.MethodEnum.DELETE,
                Url = StaticDetail.ProductApiBase + "/api/product/" + productId
            });
        }

        public async Task<ResponseDto?> GetAllProductListAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetail.MethodEnum.GET,
                Url = StaticDetail.ProductApiBase + "/api/product"
            });
        }

        public async Task<ResponseDto?> GetProductByIdAsync(int productId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetail.MethodEnum.GET,
                Url = StaticDetail.ProductApiBase + "/api/product/" + productId
            });
        }

        public async Task<ResponseDto?> UpdateProductAsync(ProductDto product)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetail.MethodEnum.PUT,
                Data = product,
                Url = StaticDetail.ProductApiBase + "/api/product"
            });
        }
    }
}
