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
    public static class PostResolver
    {
        [ExtendObjectType(Constants.GraphQLOperationTypes.Query)]
        public class PostQuery
        {
            private readonly MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Post, PostDTO>();
            });

            [UseDbContext(typeof(AppDbContext))]
            [UseProjection]
            [UseFiltering]
            [UseSorting]
            public IQueryable<PostDTO> GetPosts([ScopedService] AppDbContext dbContext) =>
                dbContext.Posts.ProjectTo<PostDTO>(mapperConfiguration);
        }
    }
}
