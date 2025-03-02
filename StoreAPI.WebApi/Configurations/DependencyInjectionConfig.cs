using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Interfaces;
using StoreAPI.Core.Services;
using StoreAPI.Infraestructure.EntityFramework.Context;
using StoreAPI.Infraestructure.Interfaces;
using StoreAPI.Infraestructure.Repositores;
using AutoMapper;
using StoreAPI.Infraestructure.EntityFramework.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using static StoreAPI.Infraestructure.EntityFramework.Context.ApplicationDbContext;

namespace StoreAPI.WebApi.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Automapper
            services.AddAutoMapper(typeof(DependencyInjectionConfig));

            // Configurar DbContext con SQL Server
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Configurar Identity
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true; // Asegura que los emails sean únicos
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Repositories and UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();

            // Services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
    