using Microsoft.EntityFrameworkCore;
using StoreAPI.Infraestructure.EntityFramework.Daos;

namespace StoreAPI.Infraestructure.EntityFramework.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }

    }
}
