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
            #endregion

        }
    }
}
