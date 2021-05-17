using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using System.Linq;
using TradeGameCRAPI.Contexts;
using TradeGameCRAPI.Entities;
using TradeGameCRAPI.Models;

namespace TradeGameCRAPI.Resolvers
{
    public static class DealResolver
    {
        [ExtendObjectType(Constants.GraphQLOperationTypes.Query)]
        public class DealQuery
        {
            private readonly MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Deal, DealDTO>();
            });

            [UseDbContext(typeof(AppDbContext))]
            [UseProjection]
            [UseFiltering]
            [UseSorting]
            public IQueryable<DealDTO> GetDeals([ScopedService] AppDbContext dbContext) =>
                dbContext.Deals.ProjectTo<DealDTO>(mapperConfiguration);
        }
    }
}
