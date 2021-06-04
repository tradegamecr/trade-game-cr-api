using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TradeGameCRAPI.Contexts;
using TradeGameCRAPI.Entities;
using TradeGameCRAPI.Helpers;
using TradeGameCRAPI.Models;
using TradeGameCRAPI.Services;

namespace TradeGameCRAPI.Resolvers
{
    public static class ProductResolver
    {
        private static readonly MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, UserDTO>();
            cfg.CreateMap<Post, PostDTO>();
            cfg.CreateMap<Product, ProductDTO>().ReverseMap();
            cfg.CreateMap<CreateProductInput, Product>();
            cfg.CreateMap<UpdateProductInput, Product>()
                .ForAllMembers(o => o.UseDestinationValue());
        });

        [Authorize]
        [ExtendObjectType(Constants.GraphQLOperationTypes.Query)]
        public class ProductQuery
        {
            [UseDbContext(typeof(AppDbContext))]
            [UsePaging]
            [UseProjection]
            [UseFiltering]
            [UseSorting]
            public IQueryable<ProductDTO> GetProducts([ScopedService] AppDbContext dbContext) =>
                dbContext.Products.ProjectTo<ProductDTO>(mapperConfiguration);

            [UseDbContext(typeof(AppDbContext))]
            [UseFirstOrDefault]
            public IQueryable<ProductDTO> GetProductById([ScopedService] AppDbContext dbContext, int id) =>
                dbContext.Products.Where(x => x.Id == id).ProjectTo<ProductDTO>(mapperConfiguration);
        }

        [Authorize]
        [ExtendObjectType(Constants.GraphQLOperationTypes.Mutation)]
        public class ProductMutation
        {
            private readonly MutationBase<Product, ProductDTO, CreateProductInput, UpdateProductInput> mutationBase;

            public ProductMutation()
            {
                mutationBase = new MutationBase<Product, ProductDTO, CreateProductInput, UpdateProductInput>
                    (mapperConfiguration.CreateMapper());
            }

            [UseDbContext(typeof(AppDbContext))]
            public async Task<ProductDTO> CreateProduct
                ([ScopedService] AppDbContext dbContext, CreateProductInput input)
            {
                var userValidatorService = new UserValidatorService(dbContext);
                var error = await userValidatorService.Exist(input.UserId);

                if (error != null)
                {
                    throw error;
                }

                return await mutationBase.Create(dbContext, input);
            }

            [UseDbContext(typeof(AppDbContext))]
            public async Task<ProductDTO> UpdateProduct
                ([ScopedService] AppDbContext dbContext, UpdateProductInput input)
            {
                var userValidatorService = new UserValidatorService(dbContext);
                var error = await userValidatorService.Exist(input.UserId);

                if (error != null)
                {
                    throw error;
                }

                return await mutationBase.Update(dbContext, input);
            }

            [UseDbContext(typeof(AppDbContext))]
            public async Task<ProductDTO> DeleteProduct
                ([ScopedService] AppDbContext dbContext, int id)
            {
                var product = await dbContext.Products
                    .AsNoTracking()
                    .Where(x => x.Id == id)
                    .Include(x => x.Post)
                    .FirstOrDefaultAsync();

                if (product.Post != null)
                {
                    throw QueryExceptionBuilder.Custom(
                        "That product is part of a post",
                        Constants.GraphQLExceptionCodes.BadRequest);
                }

                return await mutationBase.Delete(dbContext, id);
            }
        }
    }
}
