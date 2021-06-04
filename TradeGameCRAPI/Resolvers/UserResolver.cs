using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.AspNetCore.Identity;
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
            cfg.CreateMap<User, UserDTO>()
                .ForAllMembers(o => o.UseDestinationValue());
            cfg.CreateMap<CreateUserInput, User>()
                .ForAllMembers(o => o.UseDestinationValue());
            cfg.CreateMap<UpdateUserInput, User>()
                .ForAllMembers(o => o.UseDestinationValue());
        });

        [Authorize]
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

        [Authorize]
        [ExtendObjectType(Constants.GraphQLOperationTypes.Mutation)]
        public class UserMutation
        {
            private readonly IMapper mapper = mapperConfiguration.CreateMapper();

            public async Task<UserDTO> CreateUser([Service] UserManager<User> userManager, CreateUserInput input)
            {
                var user = mapper.Map<User>(input);

                user.UserName = user.Email;

                var result = await userManager.CreateAsync(user);

                if (result.Errors.Any())
                {
                    var error = result.Errors.First().Description;

                    throw QueryExceptionBuilder.Custom(error, Constants.GraphQLExceptionCodes.BadRequest);
                }

                var userDto = mapper.Map<UserDTO>(user);

                return userDto;
            }

            public async Task<UserDTO> UpdateUser([Service] UserManager<User> userManager, UpdateUserInput input)
            {
                var user = await userManager.FindByIdAsync(input.Id.ToString());

                if (user == null)
                {
                    throw QueryExceptionBuilder.NotFound<User>(input.Id);
                }

                var entityToUpdate = mapper.Map(input, user);
                var result = await userManager.UpdateAsync(entityToUpdate);

                if (result.Errors.Any())
                {
                    var error = result.Errors.First().Description;

                    throw QueryExceptionBuilder.Custom(error, Constants.GraphQLExceptionCodes.BadRequest);
                }

                var userDto = mapper.Map<UserDTO>(entityToUpdate);

                return userDto;
            }

            public async Task<UserDTO> DeleteUser([Service] UserManager<User> userManager,  int id)
            {
                var user = await userManager.FindByIdAsync(id.ToString());

                if (user == null)
                {
                    throw QueryExceptionBuilder.NotFound<User>(id);
                }

                var userDto = mapper.Map<UserDTO>(user);
                var result = await userManager.DeleteAsync(user);

                if (result.Errors.Any())
                {
                    var error = result.Errors.First().Description;

                    throw QueryExceptionBuilder.Custom(error, Constants.GraphQLExceptionCodes.BadRequest);
                }

                return userDto;
            }
        }
    }
}
