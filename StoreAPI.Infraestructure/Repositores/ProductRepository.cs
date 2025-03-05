using Microsoft.EntityFrameworkCore;
using StoreAPI.Common.Dtos;
using StoreAPI.Infraestructure.EntityFramework.Context;
using StoreAPI.Infraestructure.EntityFramework.Daos;
using StoreAPI.Infraestructure.Interfaces;


namespace StoreAPI.Infraestructure.Repositores
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetAllFeaturedAsync()
        {
            return await _context.Products.Where(p => p.IsFeatured == true).ToListAsync();
        }

        public async Task<Product> GetProduct(int productId)
        {
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null)
            {
                throw new ArgumentNullException("ProductId is null");            }
            return product;
        }

        public async Task<PagedResponseKeyset<Product>> GetWithKeysetPagination(int reference, int pageSize)
        {
            var products = await _context.Products.AsNoTracking()
                .OrderBy(x => x.Id)
                .Where(p => p.Id > reference)
                .Take(pageSize)
                .ToListAsync();

            var newReference = products.Count != 0 ? products.Last().Id : 0;

            var pagedResponse = new PagedResponseKeyset<Product>(products, newReference);

            return pagedResponse;
        }

        public async Task<Product> GetByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));

            Product? product = await _dbSet.FirstOrDefaultAsync(p => p.Name == name);

            if (product == null) throw new ArgumentException(nameof(product));

            return product;
        }

        public async Task<PagedList<Product>> GetAllPagedProducts (int skip, int take)
        {
            var products = await _context.Products.Skip(skip).Take(take).ToListAsync();
            var productsCount = products.Count;

            var pagedList = new PagedList<Product>()
            {
                Entities = products,
                HasNext = (skip + 10 < productsCount),
                HasPrevious = (skip > 0)
            };

            return pagedList;
        }
    }
}
