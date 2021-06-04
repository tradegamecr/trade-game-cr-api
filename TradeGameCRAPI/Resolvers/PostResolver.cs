using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using Nest;
using System.Linq;
using System.Threading.Tasks;
using TradeGameCRAPI.Contexts;
using TradeGameCRAPI.Entities;
using TradeGameCRAPI.Enums;
using TradeGameCRAPI.Helpers;
using TradeGameCRAPI.Models;
using TradeGameCRAPI.Services;

namespace TradeGameCRAPI.Resolvers
{
    public static class PostResolver
    {
        private static readonly MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Product, ProductDTO>().ReverseMap();
            cfg.CreateMap<Post, ESPost>();
            cfg.CreateMap<Post, PostDTO>().ReverseMap();
            cfg.CreateMap<CreatePostInput, Post>();
            cfg.CreateMap<UpdatePostInput, Post>()
                .ForAllMembers(o => o.UseDestinationValue());
        });

        [Authorize]
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

        [Authorize]
        [ExtendObjectType(Constants.GraphQLOperationTypes.Mutation)]
        public class PostMutation
        {
            private readonly MutationBase<Post, PostDTO, CreatePostInput, UpdatePostInput> mutationBase;
            private readonly IMapper mapper = mapperConfiguration.CreateMapper();

            public PostMutation()
            {
                mutationBase = new MutationBase<Post, PostDTO, CreatePostInput, UpdatePostInput>
                    (mapperConfiguration.CreateMapper());
            }

            [UseDbContext(typeof(AppDbContext))]
            public async Task<PostDTO> CreatePost
                ([ScopedService] AppDbContext dbContext, CreatePostInput input)
            {
                var userValidatorService = new UserValidatorService(dbContext);
                var error = await userValidatorService.ForPost(input.UserId, input.ProductsId);

                if (error != null)
                {
                    throw error;
                }

                var post = mapper.Map<Post>(input);
                var products = await dbContext.Products.Where(x => input.ProductsId.Contains(x.Id)).ToListAsync();

                foreach (var product in products)
                {
                    if (product.Type != ProductType.Sell)
                    {
                        throw QueryExceptionBuilder.Custom(
                            "The post are only for SELL products",
                            Constants.GraphQLExceptionCodes.BadRequest);
                    }
                }

                dbContext.Posts.Add(post);
                await dbContext.SaveChangesAsync();

                foreach (var product in products) {
                    product.PostId = post.Id;
                }

                await dbContext.SaveChangesAsync();

                var postDto = mapper.Map<PostDTO>(post);

                return postDto;
            }

            [UseDbContext(typeof(AppDbContext))]
            public async Task<PostDTO> UpdatePost
                ([ScopedService] AppDbContext dbContext, UpdatePostInput input)
            {
                var userValidatorService = new UserValidatorService(dbContext);
                var error = await userValidatorService.ForPost(input.UserId, input.ProductsId);

                if (error != null)
                {
                    throw error;
                }

                return await mutationBase.Update(dbContext, input);
            }

            [UseDbContext(typeof(AppDbContext))]
            public async Task<PostDTO> DeletePost
                ([ScopedService] AppDbContext dbContext, [Service] IElasticClient elasticClient, int id)
            {
                var post = await dbContext.Posts
                    .Where(x => x.Id == id)
                    .Include(x => x.Products)
                    .Include(x => x.User)
                    .FirstOrDefaultAsync();

                if (post == null)
                {
                    throw QueryExceptionBuilder.NotFound<Post>(id);
                }

                foreach (var product in post.Products)
                {
                    product.Type = ProductType.Backlog;
                }

                if (post.Products != null)
                {
                    post.Products.Clear();
                }

                post.User = null;

                dbContext.Posts.Remove(post);
                await dbContext.SaveChangesAsync();

                var postDto = mapper.Map<PostDTO>(post);

                return postDto;
            }
        }
    }
}
