using TradeGameCRAPI.Contexts;
using TradeGameCRAPI.Entities;

namespace TradeGameCRAPI.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(AppDbContext AppDbContext) : base(AppDbContext) { }
    }
}
