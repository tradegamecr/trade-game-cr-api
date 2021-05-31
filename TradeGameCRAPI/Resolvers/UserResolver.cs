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
    public static class UserResolver
    {
        private static readonly MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Product, ProductDTO>();
            cfg.CreateMap<Post, PostDTO>();
            cfg.CreateMap<Deal, DealDTO>();
            cfg.CreateMap<User, UserDTO>();
            cfg.CreateMap<CreateUserInput, User>();
            cfg.CreateMap<UpdateUserInput, User>()
                .ForAllMembers(o => o.UseDestinationValue());
        });

        [ExtendObjectType(Constants.GraphQLOperationTypes.Query)]
        public class UserQuery
        {
            [UseDbContext(typeof(AppDbContext))]
            [UsePaging]
            [UseProjection]
            [UseFiltering]
            [UseSorting]
            public IQueryable<UserDTO> GetUsers([ScopedService] AppDbContext dbContext) =>
                dbContext.Users.ProjectTo<UserDTO>(mapperConfiguration);

            [UseDbContext(typeof(AppDbContext))]
            [UseFirstOrDefault]
            public IQueryable<UserDTO> GetUserById([ScopedService] AppDbContext dbContext, int id) =>
                dbContext.Users.Where(x => x.Id == id).ProjectTo<UserDTO>(mapperConfiguration);
        }

        [ExtendObjectType(Constants.GraphQLOperationTypes.Mutation)]
        public class UserMutation
        {
            private readonly MutationBase<User, UserDTO, CreateUserInput, UpdateUserInput> mutationBase;

            public UserMutation()
            {
                mutationBase = new MutationBase<User, UserDTO, CreateUserInput, UpdateUserInput>
                    (mapperConfiguration.CreateMapper());
            }

            [UseDbContext(typeof(AppDbContext))]
            public async Task<UserDTO> CreateUser([ScopedService] AppDbContext dbContext, CreateUserInput input)
            {
                return await mutationBase.Create(dbContext, input);
            }

            [UseDbContext(typeof(AppDbContext))]
            public async Task<UserDTO> UpdateUser([ScopedService] AppDbContext dbContext, UpdateUserInput input)
            {
                return await mutationBase.Update(dbContext, input);
            }

            [UseDbContext(typeof(AppDbContext))]
            public async Task<UserDTO> DeleteUser
                ([ScopedService] AppDbContext dbContext, int id)
            {
                return await mutationBase.Delete(dbContext, id);
            }
        }
    }
}
