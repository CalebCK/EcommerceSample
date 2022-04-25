using Ecommerce.Domain.V1.MapProfiles;
using Ecommerce.Domain.V1.Services;
using Ecommerce.Domain.V1.Services.IServices;
using Ecommerce.Repository.Repositories;
using Ecommerce.Repository.Repositories.IRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Api.IoC
{
    /// <summary>
    /// An extension for Dependency Injections
    /// </summary>
    public static class DependencyContainer
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
        }
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IProductRepository, ProductRepository>();

        }

        public static void RegisterMapProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ProductProfile));
        }
    }
}
