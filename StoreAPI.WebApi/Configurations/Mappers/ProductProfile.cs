using AutoMapper;
using StoreAPI.Common.Dtos;
using StoreAPI.Infraestructure.EntityFramework.Daos;

namespace StoreAPI.WebApi.Configuration.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            #region
            CreateMap<Product, ProductResultDto>();

            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignoramos el Id si es autogenerado por la BD
                .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid != Guid.Empty ? src.Guid : Guid.NewGuid()));

            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();

            CreateMap<Product, ProductHomeDto>();
            CreateMap<ProductHomeDto, Product>();

            CreateMap<Product, ProductDetailDto>();
            CreateMap<ProductDetailDto, Product>();

            #endregion

        }
    }
}
