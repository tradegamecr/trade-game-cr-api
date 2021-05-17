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
    public static class UserResolver
    {
        [ExtendObjectType(Constants.GraphQLOperationTypes.Query)]
        public class UserQuery
        {
            private readonly MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>();
            });

            [UseDbContext(typeof(AppDbContext))]
            [UseProjection]
            [UseFiltering]
            [UseSorting]
            public IQueryable<UserDTO> GetUsers([ScopedService] AppDbContext dbContext) =>
                dbContext.Users.ProjectTo<UserDTO>(mapperConfiguration);
        }
    }
}
