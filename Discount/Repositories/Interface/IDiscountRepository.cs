using Discount.Entities;
using Discount.Entities.Dto;

namespace Discount.Repositories.Interface
{
    public interface IDiscountRepository
    {
        Task<List<Coupon?>> GetCouponList();

        Task<Coupon?> GetCouponById(int couponId);

        Task<Coupon?> CreateCoupon(Coupon coupon);

        Task<Coupon?> UpdateCoupon(Coupon coupon);

        Task<bool> DeleteCoupon(int couponId);

        Task<Coupon?> GetCouponByCode(string couponCode);
    }
}
