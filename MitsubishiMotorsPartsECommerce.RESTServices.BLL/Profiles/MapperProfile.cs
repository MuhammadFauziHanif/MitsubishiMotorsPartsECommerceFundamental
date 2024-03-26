using AutoMapper;
using MitsubishiMotorsPartsECommerce.RESTServices.BLL.DTOs;
using MitsubishiMotorsPartsECommerce.Domain.Models;

namespace MitsubishiMotorsPartsECommerce.BLL.Profiles
{
    public class MapperProfile : Profile
    {

        public MapperProfile()
        {
            CreateMap<ProductCategory, CategoryDTO>().ReverseMap();
            CreateMap<CategoryCreateDTO, ProductCategory>();
            CreateMap<CategoryUpdateDTO, ProductCategory>();

            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<ProductCreateDTO, Product>();
            CreateMap<ProductUpdateDTO, Product>();

        }
    }
}
