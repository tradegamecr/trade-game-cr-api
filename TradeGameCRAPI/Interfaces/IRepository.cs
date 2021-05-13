using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeGameCRAPI.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAll(bool track = false);

        Task<TEntity> Get(int id);

        Task<TEntity> Add(TEntity entity);

        Task<TEntity> Update(TEntity entity);

        Task<TEntity> Delete(int id);

        void SaveChangesAsync();
    }
}
