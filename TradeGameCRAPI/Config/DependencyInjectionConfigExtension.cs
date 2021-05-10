using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeGameCRAPI.Repositories;
using TradeGameCRAPI.Services;

namespace TradeGameCRAPI.Config
{
    public static class DependencyInjectionConfigExtension
    {
        public static void AddDependencyInjectionConfig(this IServiceCollection services)
        {
            services.AddTransient<UserRepository>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
