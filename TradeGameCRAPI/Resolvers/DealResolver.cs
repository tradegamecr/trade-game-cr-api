using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using System.Linq;
using System.Threading.Tasks;
using TradeGameCRAPI.Contexts;
using TradeGameCRAPI.Entities;
using TradeGameCRAPI.Helpers;
using TradeGameCRAPI.Models;

namespace TradeGameCRAPI.Resolvers
{
    public static class DealResolver
    {
        private static readonly MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Deal, DealDTO>();
            cfg.CreateMap<CreateDealInput, Deal>();
            cfg.CreateMap<UpdateDealInput, Deal>()
                .ForAllMembers(o => o.UseDestinationValue());
        });

        [ExtendObjectType(Constants.GraphQLOperationTypes.Query)]
        public class DealQuery
        {
            [UseDbContext(typeof(AppDbContext))]
            [UsePaging]
            [UseProjection]
            [UseFiltering]
            [UseSorting]
            public IQueryable<DealDTO> GetDeals([ScopedService] AppDbContext dbContext) =>
                dbContext.Deals.ProjectTo<DealDTO>(mapperConfiguration);

            [UseDbContext(typeof(AppDbContext))]
            [UseFirstOrDefault]
            public IQueryable<DealDTO> GetDealById([ScopedService] AppDbContext dbContext, int id) =>
                dbContext.Deals.Where(x => x.Id == id).ProjectTo<DealDTO>(mapperConfiguration);
        }

        [ExtendObjectType(Constants.GraphQLOperationTypes.Mutation)]
        public class DealMutation
        {
            private readonly MutationBase<Deal, DealDTO, CreateDealInput, UpdateDealInput> mutationBase;

            public DealMutation()
            {
                mutationBase = new MutationBase<Deal, DealDTO, CreateDealInput, UpdateDealInput>
                    (mapperConfiguration.CreateMapper());
            }

            [UseDbContext(typeof(AppDbContext))]
            public async Task<DealDTO> CreateDeal([ScopedService] AppDbContext dbContext, CreateDealInput input)
            {
                return await mutationBase.Create(dbContext, input);
            }

            [UseDbContext(typeof(AppDbContext))]
            public async Task<DealDTO> UpdateDeal([ScopedService] AppDbContext dbContext, UpdateDealInput input)
            {
                return await mutationBase.Update(dbContext, input);
            }

            [UseDbContext(typeof(AppDbContext))]
            public async Task<DealDTO> DeleteDeal([ScopedService] AppDbContext dbContext, int id)
            {
                return await mutationBase.Delete(dbContext, id);
            }
        }
    }
}
