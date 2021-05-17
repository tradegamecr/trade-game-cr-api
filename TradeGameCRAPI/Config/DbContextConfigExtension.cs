using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TradeGameCRAPI.Contexts;

namespace TradeGameCRAPI.Config
{
    public static class DbContextConfigExtension
    {
        public static void AddAppDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddPooledDbContextFactory<AppDbContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
