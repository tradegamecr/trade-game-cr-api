using Microsoft.Extensions.DependencyInjection;
using TradeGameCRAPI.Repositories;
using TradeGameCRAPI.Services;

namespace TradeGameCRAPI.Config
{
    public static class DependencyInjectionConfigExtension
    {
        public static void AddDependencyInjectionConfig(this IServiceCollection services)
        {
            services.AddTransient<UserRepository>();
            services.AddTransient<UserService>();
        }
    }
}
