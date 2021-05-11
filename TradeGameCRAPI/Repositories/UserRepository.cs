using TradeGameCRAPI.Contexts;
using TradeGameCRAPI.Entities;
using TradeGameCRAPI.Interfaces;

namespace TradeGameCRAPI.Repositories
{
    public class UserRepository : BaseRepository<User, AppDbContext>, IBaseRepository<User>
    {
        public UserRepository(AppDbContext AppDbContext) : base(AppDbContext) { }
    }
}
