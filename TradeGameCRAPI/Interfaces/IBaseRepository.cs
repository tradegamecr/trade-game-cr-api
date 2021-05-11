using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeGameCRAPI.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        Task<List<TEntity>> GetAll();

        Task<TEntity> Get(int id);

        Task<TEntity> FirstOrDefaultAsync(int id);

        Task<TEntity> Add(TEntity entity);

        Task<TEntity> Update(TEntity entity);

        Task<TEntity> Delete(int id);

        Task SaveChangesAsync();
    }
}
