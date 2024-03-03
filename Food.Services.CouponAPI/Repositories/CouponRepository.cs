using Food.Services.CouponAPI.Data;
using Food.Services.CouponAPI.Models;
using Food.Services.CouponAPI.Models.Dto;
using Food.Services.CouponAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace Food.Services.CouponAPI.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly CouponDbContext _dbContext;

        public CouponRepository(CouponDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<List<Coupon>> GetCouponList()
        {             
            List<Coupon> coupons =  await _dbContext.Coupons.ToListAsync();
            return coupons;
        }

        public async Task<Coupon> GetCouponById(int couponId)
        {
            return await _dbContext.Coupons.SingleOrDefaultAsync(x => x.CouponId == couponId);
        }

        public async Task<Coupon> AddCoupon(Coupon coupon)
        {           
            await _dbContext.Coupons.AddAsync(coupon);
            _dbContext.SaveChanges();
            return coupon;
        }

        public async Task<Coupon> UpdateCoupon(Coupon coupon)
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
    }
}
