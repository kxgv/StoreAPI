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
                .ForMember(dest => dest.PublicId, opt => opt.MapFrom(src => src.Guid != Guid.Empty ? src.Guid : Guid.NewGuid()));

            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();

            #endregion

        }
    }
}
