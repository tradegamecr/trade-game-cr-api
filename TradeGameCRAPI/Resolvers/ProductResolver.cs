using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Nest;
using System.Linq;
using System.Threading.Tasks;
using TradeGameCRAPI.Contexts;
using TradeGameCRAPI.Entities;
using TradeGameCRAPI.Helpers;
using TradeGameCRAPI.Models;

namespace TradeGameCRAPI.Resolvers
{
    public static class ProductResolver
    {
        private static readonly MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Product, ProductDTO>();
            cfg.CreateMap<CreateProductInput, Product>();
            cfg.CreateMap<UpdateProductInput, Product>();
        });

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
                ([ScopedService] AppDbContext dbContext, [Service] IElasticClient elasticClient, CreateProductInput input)
            {
                return await mutationBase.Create(dbContext, input);
            }

            [UseDbContext(typeof(AppDbContext))]
            public async Task<ProductDTO> UpdateProduct([ScopedService] AppDbContext dbContext, UpdateProductInput input)
            {
                return await mutationBase.Update(dbContext, input);
            }

            [UseDbContext(typeof(AppDbContext))]
            public async Task<ProductDTO> DeleteProduct([ScopedService] AppDbContext dbContext, int id)
            {
                return await mutationBase.Delete(dbContext, id);
            }
        }
    }
}
