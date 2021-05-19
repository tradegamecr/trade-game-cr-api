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
    public static class PostResolver
    {
        private static readonly MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Post, PostDTO>();
            cfg.CreateMap<CreatePostInput, Post>();
            cfg.CreateMap<UpdatePostInput, Post>();
        });

        [ExtendObjectType(Constants.GraphQLOperationTypes.Query)]
        public class PostQuery
        {
            [UseDbContext(typeof(AppDbContext))]
            [UsePaging]
            [UseProjection]
            [UseFiltering]
            [UseSorting]
            public IQueryable<PostDTO> GetPosts([ScopedService] AppDbContext dbContext) =>
                dbContext.Posts.ProjectTo<PostDTO>(mapperConfiguration);

            [UseDbContext(typeof(AppDbContext))]
            [UseFirstOrDefault]
            public IQueryable<PostDTO> GetPostById([ScopedService] AppDbContext dbContext, int id) =>
                dbContext.Posts.Where(x => x.Id == id).ProjectTo<PostDTO>(mapperConfiguration);
        }

        [ExtendObjectType(Constants.GraphQLOperationTypes.Mutation)]
        public class PostMutation
        {
            private readonly MutationBase<Post, PostDTO, CreatePostInput, UpdatePostInput> mutationBase;

            public PostMutation()
            {
                mutationBase = new MutationBase<Post, PostDTO, CreatePostInput, UpdatePostInput>
                    (mapperConfiguration.CreateMapper());
            }

            [UseDbContext(typeof(AppDbContext))]
            public async Task<PostDTO> CreatePost([ScopedService] AppDbContext dbContext, CreatePostInput input)
            {
                return await mutationBase.Create(dbContext, input);
            }

            [UseDbContext(typeof(AppDbContext))]
            public async Task<PostDTO> UpdatePost([ScopedService] AppDbContext dbContext, UpdatePostInput input)
            {
                return await mutationBase.Update(dbContext, input);
            }

            [UseDbContext(typeof(AppDbContext))]
            public async Task<PostDTO> DeletePost([ScopedService] AppDbContext dbContext, int id)
            {
                return await mutationBase.Delete(dbContext, id);
            }
        }
    }
}
