using AutoMapper;
using Backend.DTO;
using Backend.Models;

namespace Backend.AutoMapper
{
    using AutoMapper;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); 

            CreateMap<Order, OrderDTO>()
                //.ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name)) 
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}