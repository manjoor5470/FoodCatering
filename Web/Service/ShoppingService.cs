using Microsoft.AspNetCore.Mvc.Formatters;
using Web.Models;
using Web.Service.IService;
using Web.Utility;

namespace Web.Service
{
    public class ShoppingService : IShoppingService
    {
        private readonly IBaseService _baseService;

        public ShoppingService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetail.MethodEnum.POST,
                Data = cartDto,
                Url = StaticDetail.ShoppingApiBase + "/api/cart/ApplyCoupon"
            });
        }

        public async Task<ResponseDto?> CartDetailsAsync(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetail.MethodEnum.GET,
                Url = StaticDetail.ShoppingApiBase + "/api/cart/CartDetails/" + userId
            });
        }

        public async Task<ResponseDto?> CreateUpSertAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetail.MethodEnum.POST,
                Data = cartDto,
                Url = StaticDetail.ShoppingApiBase + "/api/cart/CreateUpSert"
            });
        }

        public async Task<ResponseDto?> RemoveCartAsync(int cartDetailId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetail.MethodEnum.POST,
                Data = cartDetailId,
                Url = StaticDetail.ShoppingApiBase + "/api/cart/RemoveCart"
            });
        }

        public async Task<ResponseDto?> RemoveCouponAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetail.MethodEnum.POST,
                Data = cartDto,
                Url = StaticDetail.ShoppingApiBase + "/api/cart/RemoveCoupon"
            });
        }
    }
}
