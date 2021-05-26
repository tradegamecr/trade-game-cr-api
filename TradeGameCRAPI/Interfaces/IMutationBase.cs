using System.Threading.Tasks;
using TradeGameCRAPI.Contexts;

namespace TradeGameCRAPI.Interfaces
{
    public interface IMutationBase<TEntity, TEntityDTO, TCreatInput, TUpdateInput>
    {
        public Task<TEntityDTO> Create(AppDbContext dbContext, TCreatInput input);

        public Task<TEntityDTO> Update(AppDbContext dbContext, TUpdateInput input);

        public Task<TEntityDTO> Delete(AppDbContext dbContext, int id);
    }
}
