using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;

namespace TradeGameCRAPI.Config
{
    public static class ElasticSearchExtension
    {
        public static void ConfigElasticClient(this IServiceCollection services, string uri)
        {
            var node = new Uri(uri);
            var settings = new ConnectionSettings(node);
            var client = new ElasticClient(settings);

            services.AddSingleton<IElasticClient>(client);

        }
    }
}
