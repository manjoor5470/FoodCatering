using AutoMapper;
using Discount.Entities;
using Discount.Entities.Dto;

namespace Discount.Mapper
{
    public class DiscountProfile: Profile
    {
        public DiscountProfile()
        {
            CreateMap<Coupon, CouponDto>().ReverseMap();
        }        
    }
}
