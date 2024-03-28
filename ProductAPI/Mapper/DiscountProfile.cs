using AutoMapper;
using ProductAPI.Entities;
using ProductAPI.Entities.Dto;

namespace Discount.Mapper
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductModel, ProductDto>().ReverseMap();
        }        
    }
}
