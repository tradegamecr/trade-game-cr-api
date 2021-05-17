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
    public static class ProductResolver
    {
        [ExtendObjectType(Constants.GraphQLOperationTypes.Query)]
        public class ProductQuery
        {
            private readonly MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDTO>();
            });

            [UseDbContext(typeof(AppDbContext))]
            [UseProjection]
            [UseFiltering]
            [UseSorting]
            public IQueryable<ProductDTO> GetProducts([ScopedService] AppDbContext dbContext) =>
                dbContext.Products.ProjectTo<ProductDTO>(mapperConfiguration);
        }
    }
}
