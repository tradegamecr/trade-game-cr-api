﻿using Elasticsearch.Net;
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
            var settings = new ConnectionSettings(node)
                .BasicAuthentication("elastic", "changeme");
            var client = new ElasticClient(settings);

            client.Indices.Create(Constants.ESIndexes.Cards);

            services.AddSingleton<IElasticClient>(client);
        }
    }
}
