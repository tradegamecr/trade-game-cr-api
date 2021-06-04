using AutoMapper;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using Nest;
using System.Threading.Tasks;
using TradeGameCRAPI.Helpers;
using TradeGameCRAPI.Models;

namespace TradeGameCRAPI.Resolvers
{
    public static class SearchResolver
    {
        [Authorize]
        [ExtendObjectType(Constants.GraphQLOperationTypes.Query)]
        public class SearchQuery
        {
            private readonly IMapper mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ESCard, CardDTO>();
                cfg.CreateMap<ESImageUris, ImageUrisDTO>();
                cfg.CreateMap<ESRelatedUris, RelatedUrisDTO>();
            }).CreateMapper();

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

            public async Task<CardDTO> SearchById([Service] IElasticClient elasticClient, string id)
            {
                var searchResponse = await elasticClient.SourceAsync<ESCard>
                    (id, s => s.Index(Constants.ESIndexes.Cards));

                if (searchResponse.ApiCall.HttpStatusCode == 404)
                {
                    throw QueryExceptionBuilder.Custom
                        ($"Card with the Id {id} not found", Constants.GraphQLExceptionCodes.NotFound);
                }

                if (!searchResponse.IsValid)
                {
                    throw QueryExceptionBuilder.BadRequest();
                }

                var cardDto = mapper.Map<CardDTO>(searchResponse.Body);

                return cardDto;
            }
        }
    }
}
