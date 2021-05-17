using Microsoft.Extensions.DependencyInjection;
using TradeGameCRAPI.Resolvers;

namespace TradeGameCRAPI.Config
{
    public static class GraphQLConfigExtension
    {
        public static void AddGraphQLConfig(this IServiceCollection services)
        {
            services
                .AddGraphQLServer()
                .AddQueryType(d => d.Name(Constants.GraphQLOperationTypes.Query))
                .AddType<UserResolver.UserQuery>()
                .AddType<ProductResolver.ProductQuery>()
                .AddType<PostResolver.PostQuery>()
                .AddType<DealResolver.DealQuery>()
                .AddFiltering()
                .AddSorting()
                .AddProjections();
        }
    }
}
