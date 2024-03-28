using Discount.Entities.Dto;
using ShoppingCartAPI.Common;

namespace ShoppingCartAPI.Repositories.Interface
{
    public interface IDiscountService
    {
        Task<CouponDto> GetCouponList(string GetCouponById);
    }
}
