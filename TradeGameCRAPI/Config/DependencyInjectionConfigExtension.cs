using Microsoft.Extensions.DependencyInjection;
using TradeGameCRAPI.Contexts;
using TradeGameCRAPI.Interfaces;
using TradeGameCRAPI.Services;

namespace TradeGameCRAPI.Config
{
    public static class DependencyInjectionConfigExtension
    {
        public static void AddDependencyInjectionConfig(this IServiceCollection services)
        {
            services.AddTransient<AppDbContext>();
            services.AddTransient<IFacebookAuthService, FacebookAuthService>();
        }
    }
}
