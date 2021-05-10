using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace TradeGameCRAPI.Config
{
    public static class SwaggerConfigExtension
    {
        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Trade GameCR API", Version = "v1" });
            });
        }

        public static void AddSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TradeGameCRAPI v1"));
        }
    }
}
