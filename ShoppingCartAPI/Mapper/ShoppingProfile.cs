using AutoMapper;
using ShoppingCartAPI.Entities;
using ShoppingCartAPI.Entities.Dto;

namespace Discount.Mapper
{
    public class ShoppingProfile: Profile
    {
        public ShoppingProfile()
        {
            CreateMap<CartDto, Cart>().ReverseMap();
            CreateMap<CartHeaderDto, CartHeader>().ReverseMap();
            CreateMap<CartDetailDto, CartDetail>().ReverseMap();
        }
    }
}
