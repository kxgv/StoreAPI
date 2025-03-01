using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Infraestructure.EntityFramework.Daos;
using static StoreAPI.Infraestructure.EntityFramework.Context.ApplicationDbContext;

namespace StoreAPI.Infraestructure.EntityFramework.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // config if needed
        }

        public virtual DbSet<Product> Products { get; set; }
        //public virtual DbSet<User>  Users { get; set; }

        public class ApplicationUser : IdentityUser
        {
            // properties
        }

    }
}
