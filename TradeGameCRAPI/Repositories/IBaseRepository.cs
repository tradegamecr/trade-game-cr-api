using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeGameCRAPI.Repositories
{
    public interface IBaseRepository<T>
    {
        public Task<List<T>> GetAll();

        public Task<T> Get(int id);

        public Task<T> FirstOrDefaultAsync(int id);

        public Task<T> Add(T entity);

        public Task<T> Update(T entity);

        public Task<T> Delete(int id);

        public Task SaveChangesAsync();
    }
}
