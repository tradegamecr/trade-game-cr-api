using AutoMapper;
using HotChocolate.Types;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TradeGameCRAPI.Models;

namespace TradeGameCRAPI.Resolvers
{
    [ExtendObjectType(Constants.GraphQLOperationTypes.Query)]
    public class BulkQuery
    {
        private string path = @"C:\Users\rcane\Desktop\projects\trade-game-cr-api\TradeGameCRAPI\Files\jsondata.json";
        private static readonly IMapper mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Card, ESCard>();
            cfg.CreateMap<ImageUris, ESImageUris>();
            cfg.CreateMap<RelatedUris, ESRelatedUris>().ReverseMap();
        }).CreateMapper();

        public async Task<string> Download()
        {
            var httpClient = new HttpClient();
            var responseStr = await httpClient.GetStringAsync
                ("https://c2.scryfall.com/file/scryfall-bulk/default-cards/default-cards-20210523210313.json");
            var cards = Card.FromJson(responseStr);
            var esCards = mapper.Map<List<ESCard>>(cards);
            var sw = new StreamWriter(path);
            var serializeOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            foreach (var esCard in esCards)
            {
                await sw.WriteLineAsync(JsonSerializer.Serialize(esCard, serializeOptions));
            }

            return "Done!";
        }
    }
}
