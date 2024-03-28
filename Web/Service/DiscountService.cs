using Web.Models;
using Web.Service.IService;
using Web.Utility;

namespace Web.Service
{
    public class DiscountService: IDiscountService
    {
        private readonly IBaseService _baseService;

        public DiscountService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> CreateCouponAsync(CouponDto coupon)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetail.MethodEnum.POST,
                Data = coupon,
                Url = StaticDetail.DiscountApiBase + "/api/coupon/"
            });
        }

        public async Task<ResponseDto?> DeleteCouponAsync(int couponId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetail.MethodEnum.DELETE,
                Url = StaticDetail.DiscountApiBase + "/api/coupon/" + couponId
            });
        }

        public async Task<ResponseDto?> GetAllCouponListAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetail.MethodEnum.GET,
                Url = StaticDetail.DiscountApiBase + "/api/coupon"
            });
        }

        public async Task<ResponseDto?> GetCouponByIdAsync(int couponId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetail.MethodEnum.GET,
                Url = StaticDetail.DiscountApiBase + "/api/coupon/" + couponId
            });
        }

        public async Task<ResponseDto?> UpdateCouponAsync(CouponDto coupon)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetail.MethodEnum.PUT,
                Data = coupon,
                Url = StaticDetail.DiscountApiBase + "/api/coupon/"
            });
        }
    }
}
