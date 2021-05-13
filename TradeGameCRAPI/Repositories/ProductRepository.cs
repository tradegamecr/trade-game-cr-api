using TradeGameCRAPI.Contexts;
using TradeGameCRAPI.Entities;
using TradeGameCRAPI.Interfaces;

namespace TradeGameCRAPI.Repositories
{
    public class ProductRepository : Repository<Product>
    {
        public ProductRepository(AppDbContext AppDbContext) : base(AppDbContext) { }
    }
}
