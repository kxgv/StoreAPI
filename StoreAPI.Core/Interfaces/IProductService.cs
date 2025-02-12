using StoreAPI.Infraestructure.EntityFramework.Daos;
using StoreAPI.Common.Dtos;

namespace StoreAPI.Core.Interfaces
{
    public interface IProductService
    {
        Task<PagedResponseKeyset<ProductResultDto>> GetWithKeysetPagination(int reference, int pageSize);
        //Task<ProductDto> GetDto(int id);
        //Task<Product> GetProductById(int id);
        Task<ProductDto> Post(int productId, ProductDto model);
        Task<IEnumerable<ProductHomeDto>> GetFeaturedProductsAsync();
        Task<ProductDetailDto> GetProductDetailAsync(int productId);
    }
}
