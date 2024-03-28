using Web.Models;

namespace Web.Service.IService
{
    public interface IDiscountService
    {
        Task<ResponseDto?> GetAllCouponListAsync();

        Task<ResponseDto?> GetCouponByIdAsync(int couponId);

        Task<ResponseDto?> CreateCouponAsync(CouponDto coupon);

        Task<ResponseDto?> UpdateCouponAsync(CouponDto coupon);

        Task<ResponseDto?> DeleteCouponAsync(int couponId);
    }
}
