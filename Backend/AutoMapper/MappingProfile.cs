using AutoMapper;
using Backend.DTO;
using Backend.Models;

namespace Backend.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();

            CreateMap<Order, OrderDTO>().ReverseMap();

            CreateMap<OrderProduct, OrderProductDTO>().ReverseMap();
    }
}