using HotChocolate;
using HotChocolate.Types;
using Nest;
using System.Threading.Tasks;
using TradeGameCRAPI.Helpers;
using TradeGameCRAPI.Models;

namespace TradeGameCRAPI.Resolvers
{
    [ExtendObjectType(Constants.GraphQLOperationTypes.Query)]
    public class SearchQuery
    {
        public async Task<SearchResponse> Search
            ([Service] IElasticClient elasticClient, string query, int? from = 0, int? size = 20)
        {
            if (from > size)
            {
                throw QueryExceptionBuilder
                    .BadPagingParams("The \"from\" value cannot be higher than \"size\" value");
            }

            var searchResponse = await elasticClient.SearchAsync<ESCard>(s => s
                    .Index(Constants.ESIndexes.Cards)
                    .From(from)
                    .Size(size)
                    .Query(q => q
                        .Bool(b => b
                            .Should(
                                m => m.MatchPhrase(mp => mp
                                    .Field(f => f.Name)
                                    .Query(query)
                                ),
                                m => m.MatchPhrase(mp => mp
                                    .Field(f => f.OracleText)
                                    .Query(query)
                                ),
                                m => m.MatchPhrase(mp => mp
                                    .Field(f => f.FlavorText)
                                    .Query(query)
                                )
                            )
                        )
                    )
                );

            if (!searchResponse.IsValid)
            {
                throw QueryExceptionBuilder.BadRequest();
            }

            var searchResponseBuilder = new SearchResponseBuilder(
                searchResponse,
                query,
                (int)from,
                (int)size);

            return searchResponseBuilder.GetResponse();
        }
    }
}
