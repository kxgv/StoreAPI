using StoreAPI.Infraestructure.EntityFramework.Daos;
using StoreAPI.Common.Dtos;

namespace StoreAPI.Core.Interfaces
{
    public interface IProductService
    {
        Task<PagedResponseKeyset<ProductResultDto>> GetWithKeysetPagination(int reference, int pageSize);
        Task<IEnumerable<ProductHomeDto>> GetFeaturedProductsAsync();
        Task<ProductDetailDto> GetProductDetailAsync(int productId);
        Task<IEnumerable<ProductHomeDto>> GetAllProductsAsync();
        Task<Product> GetProductAsync(int productId);
        Task DeleteProductAsync(int productId);
        Task<CreateProductDto> Post(CreateProductDto model);
        Task<CreateProductDto>Put(int productId, CreateProductDto model);
    }
}
