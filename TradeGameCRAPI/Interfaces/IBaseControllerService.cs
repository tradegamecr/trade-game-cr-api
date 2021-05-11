using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeGameCRAPI.Interfaces
{
    public interface IBaseControllerService<TEntity, TEntityDTO, TEntityCreateDTO, TEntityUpdateDTO>
    {
        Task<List<TEntityDTO>> GetAll();

        Task<TEntityDTO> Get(int id);

        Task<TEntity> FirstOrDefaultAsync(int id);

        Task<TEntityDTO> Add(TEntityCreateDTO createEntity);

        Task<TEntityDTO> Update(TEntityUpdateDTO updateEntity);

        Task<TEntityDTO> Delete(int id);

        Task SaveChangesAsync();

        TEntityDTO ToEntityDTO(TEntity entity);

        void MapReverse(TEntityDTO entityDto, TEntity entity);
    }
}
