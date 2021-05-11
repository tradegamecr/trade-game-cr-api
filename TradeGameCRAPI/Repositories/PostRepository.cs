using TradeGameCRAPI.Contexts;
using TradeGameCRAPI.Entities;
using TradeGameCRAPI.Interfaces;

namespace TradeGameCRAPI.Repositories
{
    public class PostRepository : BaseRepository<Post, AppDbContext>, IBaseRepository<Post>
    {
        public PostRepository(AppDbContext appDbContext) : base(appDbContext) { }
    }
}
