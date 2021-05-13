using TradeGameCRAPI.Contexts;
using TradeGameCRAPI.Entities;

namespace TradeGameCRAPI.Repositories
{
    public class DealRepository : Repository<Deal>
    {
        public DealRepository(AppDbContext AppDbContext) : base(AppDbContext) { }
    }
}
