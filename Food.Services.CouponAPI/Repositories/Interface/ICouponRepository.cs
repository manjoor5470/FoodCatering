using Food.Services.CouponAPI.Models;
using Food.Services.CouponAPI.Models.Dto;

namespace Food.Services.CouponAPI.Repositories.Interface
{
    public interface ICouponRepository
    {
        Task<List<Coupon>> GetCouponList();

        Task<Coupon> GetCouponById(int couponId);

        Task<Coupon> AddCoupon(Coupon coupon);

        Task<Coupon> UpdateCoupon(Coupon coupon);

        Task<bool> DeleteCoupon(int couponId);
    }
}
