using Microsoft.Extensions.DependencyInjection;
using TradeGameCRAPI.Entities;
using TradeGameCRAPI.Interfaces;
using TradeGameCRAPI.Repositories;

namespace TradeGameCRAPI.Config
{
    public static class DependencyInjectionConfigExtension
    {
        public static void AddDependencyInjectionConfig(this IServiceCollection services)
        {
            services.AddTransient<IRepository<User>, UserRepository>();
            services.AddTransient<IRepository<Post>, PostRepository>();
            services.AddTransient<IRepository<Product>, ProductRepository>();
            services.AddTransient<IRepository<Deal>, DealRepository>();
        }
    }
}
