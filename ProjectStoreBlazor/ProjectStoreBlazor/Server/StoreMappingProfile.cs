using AutoMapper;
using ProjectStoreBlazor.Server.Entities;
using ProjectStoreBlazor.Shared.Models;

namespace ProjectStoreBlazor.Server

{
    public class StoreMappingProfile : Profile
    {
        public StoreMappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<RegisterUserDto, User>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
        }
    }
}
