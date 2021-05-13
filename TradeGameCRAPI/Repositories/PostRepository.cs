using TradeGameCRAPI.Contexts;
using TradeGameCRAPI.Entities;

namespace TradeGameCRAPI.Repositories
{
    public class PostRepository : Repository<Post>
    {
        public PostRepository(AppDbContext AppDbContext) : base(AppDbContext) { }
    }
}
