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
                .AddAuthorization()
                .AddQueryType(x => x.Name(Constants.GraphQLOperationTypes.Query))
                .AddType<UserResolver.UserQuery>()
                .AddType<ProductResolver.ProductQuery>()
                .AddType<PostResolver.PostQuery>()
                .AddType<DealResolver.DealQuery>()
                .AddType<SearchResolver.SearchQuery>()
                .AddType<BulkResolver.BulkQuery>()
                .AddMutationType(x => x.Name(Constants.GraphQLOperationTypes.Mutation))
                .AddType<UserResolver.UserMutation>()
                .AddType<ProductResolver.ProductMutation>()
                .AddType<PostResolver.PostMutation>()
                .AddType<DealResolver.DealMutation>()
                .AddType<SignInResolver.SignInMutation>()
                .AddFiltering()
                .AddSorting()
                .AddProjections();
        }
    }
}
