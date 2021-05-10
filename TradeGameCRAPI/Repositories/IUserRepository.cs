using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeGameCRAPI.Entities;

namespace TradeGameCRAPI.Repositories
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAll();

        public Task<User> Get(int id);

        public Task<User> FirstOrDefaultAsync(int id);

        public Task<User> Add(User entity);

        public Task<User> Update(User entity);

        public Task<User> Delete(int id);

        public Task SaveChangesAsync();
    }
}
