﻿using Microsoft.Extensions.DependencyInjection;
using TradeGameCRAPI.Resolvers;

namespace TradeGameCRAPI.Config
{
    public static class GraphQLConfigExtension
    {
        public static void AddGraphQLConfig(this IServiceCollection services)
        {
            services
                .AddGraphQLServer()
                .AddQueryType(x => x.Name(Constants.GraphQLOperationTypes.Query))
                .AddType<UserResolver.UserQuery>()
                .AddType<ProductResolver.ProductQuery>()
                .AddType<PostResolver.PostQuery>()
                .AddType<DealResolver.DealQuery>()
                .AddMutationType(x => x.Name(Constants.GraphQLOperationTypes.Mutation))
                .AddType<UserResolver.UserMutation>()
                .AddType<ProductResolver.ProductMutation>()
                .AddType<PostResolver.PostMutation>()
                .AddType<DealResolver.DealMutation>()
                .AddFiltering()
                .AddSorting()
                .AddProjections();
        }
    }
}
