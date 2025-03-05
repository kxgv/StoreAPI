using StoreAPI.Infraestructure.EntityFramework.Daos;

namespace StoreAPI.Infraestructure.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<PagedResponseKeyset<Product>> GetWithKeysetPagination(int reference, int pageSize);
        Task<Product> GetByNameAsync(string name);
        Task<IEnumerable<Product>> GetAllFeaturedAsync();
        Task<Product> GetProduct(int productId);
        Task<PagedList<Product>> GetAllPagedProducts(int skip, int take);
    }
}
