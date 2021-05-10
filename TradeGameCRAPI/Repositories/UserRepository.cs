using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeGameCRAPI.Contexts;
using TradeGameCRAPI.Entities;

namespace TradeGameCRAPI.Repositories
{
    public class UserRepository : BaseRepository<User, AppDbContext>, IUserRepository
    {
        public UserRepository(AppDbContext appDbContext) : base(appDbContext) { }
    }
}
