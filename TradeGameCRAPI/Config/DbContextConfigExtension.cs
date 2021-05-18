using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using TradeGameCRAPI.Contexts;

namespace TradeGameCRAPI.Config
{
    public static class DbContextConfigExtension
    {
        public static void AddAppDbContext(this IServiceCollection services, string connectionString, bool isDev)
        {
            services.AddPooledDbContextFactory<AppDbContext>((s, o) =>
            {
                o.UseSqlServer(connectionString);

                if (isDev)
                {
                    o.LogTo(Console.WriteLine);
                }
            });
        }
    }
}
