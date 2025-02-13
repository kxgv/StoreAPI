using Microsoft.EntityFrameworkCore;
using StoreAPI.Core.Interfaces;
using StoreAPI.Core.Services;
using StoreAPI.Infraestructure.EntityFramework.Context;
using StoreAPI.Infraestructure.Interfaces;
using StoreAPI.Infraestructure.Repositores;
using AutoMapper;
using StoreAPI.Infraestructure.EntityFramework.UnitOfWork;

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

            // Repositories and UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();

            // Services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<AuthService>();

            return services;
        }
    }
}
    