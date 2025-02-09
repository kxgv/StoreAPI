using AutoMapper;
using StoreAPI.Common.Dtos;
using StoreAPI.Infraestructure.EntityFramework.Daos;

namespace StoreAPI.WebApi.Configuration.Mappers
{
    public class PagedResponseKeysetProfile : Profile
    {
        public PagedResponseKeysetProfile()
        {
            CreateMap(typeof(PagedResponseKeyset<>), typeof(PagedResponseKeysetDto<>));
        }
    }
}
