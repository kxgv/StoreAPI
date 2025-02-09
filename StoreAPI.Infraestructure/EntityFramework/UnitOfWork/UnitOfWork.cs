using StoreAPI.Infraestructure.EntityFramework.Context;
using StoreAPI.Infraestructure.EntityFramework.Daos;
using StoreAPI.Infraestructure.Interfaces;
using StoreAPI.Infraestructure.Repositores;

namespace StoreAPI.Infraestructure.EntityFramework.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly ApplicationDbContext _context;
        public IProductRepository Products { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Products = new ProductRepository(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
