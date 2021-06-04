using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TradeGameCRAPI.Contexts;
using TradeGameCRAPI.Entities;

namespace TradeGameCRAPI.Config
{
    public static class IdentityConfigExtension
    {
        public static void AddIdentityConfig(this IServiceCollection services)
        {
            services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders()
                .AddRoles<Role>()
                .AddRoleManager<RoleManager<Role>>()
                .AddRoleStore<RoleStore<Role, AppDbContext, int>>();
        }
    }
}
