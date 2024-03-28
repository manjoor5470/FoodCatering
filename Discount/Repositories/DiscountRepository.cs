using Discount.Data;
using Discount.Entities;
using Discount.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Discount.Repositories
{
    public class DiscountRepository: IDiscountRepository
    {
        private readonly DiscountDbContext _dbContext;

        public DiscountRepository(DiscountDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Coupon?>> GetCouponList()
        {
            return await _dbContext.Coupons.ToListAsync();
        }

        public async Task<Coupon?> GetCouponById(int couponId)
        {
            return await _dbContext.Coupons.SingleOrDefaultAsync(x => x.CouponId == couponId);
        }

        public async Task<Coupon?> GetCouponByCode(string couponCode)
        {
            return await _dbContext.Coupons.SingleOrDefaultAsync(x => x.CouponCode == couponCode);
        }

        public async Task<Coupon?> AddCoupon(Coupon coupon)
        {
            await _dbContext.Coupons.AddAsync(coupon);
            _dbContext.SaveChanges();
            return coupon;
        }

        public async Task<Coupon?> UpdateCoupon(Coupon coupon)
        {
            var couponDetails = await _dbContext.Coupons.FirstOrDefaultAsync(x => x.CouponId == coupon.CouponId);
            if (couponDetails != null)
            {
                couponDetails.CouponCode = coupon.CouponCode;
                couponDetails.DiscountAmount = coupon.DiscountAmount;
                couponDetails.MinAmount = coupon.MinAmount;

                _dbContext.SaveChanges();
                return couponDetails;
            }

            return coupon;
        }        

        public async Task<bool> DeleteCoupon(int couponId)
        {
            var coupon = await _dbContext.Coupons.FirstAsync(x => x.CouponId == couponId);

            if (coupon == null)
            {
                return false;
            }

            _dbContext.Coupons.Remove(coupon);
            _dbContext.SaveChanges();
            return true;
        }

        public async Task<Coupon?> CreateCoupon(Coupon coupon)
        {
            await _dbContext.Coupons.AddAsync(coupon);
            _dbContext.SaveChanges();
            return coupon;
        }
    }
}
