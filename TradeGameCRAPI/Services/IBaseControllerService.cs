using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeGameCRAPI.Repositories;

namespace TradeGameCRAPI.Services
{
    public interface IBaseControllerService<TEntity, TEntityDTO, TEntityCreateDTO, TEntityUpdateDTO, TRepository>
        where TEntity : class
        where TEntityDTO : class
        where TEntityCreateDTO : class
        where TEntityUpdateDTO : class
        where TRepository : IBaseRepository<TEntity>
    {
        public Task<List<TEntityDTO>> GetAll();

        public Task<TEntityDTO> Get(int id);

        public Task<TEntity> FirstOrDefaultAsync(int id);

        public Task<TEntityDTO> Add(TEntityCreateDTO entity);

        public Task<TEntityDTO> Update(TEntityUpdateDTO entity);

        public Task<TEntityDTO> Delete(int id);

        public Task SaveChangesAsync();

        public TEntityDTO ToEntityDTO(TEntity entity);

        public void MapReverse(TEntityDTO entityDto, TEntity entity);
    }
}
