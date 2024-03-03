using AutoMapper;
using Food.Services.CouponAPI.Models;
using Food.Services.CouponAPI.Models.Dto;

namespace Food.Services.CouponAPI.Mapper
{
    public class CouponProfile: Profile
    {
        public CouponProfile()
        {
            CreateMap<Coupon, CouponDto>().ReverseMap();
        }
    }
}
