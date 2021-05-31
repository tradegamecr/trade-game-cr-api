using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TradeGameCRAPI.Contexts;
using TradeGameCRAPI.Entities;
using TradeGameCRAPI.Interfaces;

namespace TradeGameCRAPI.Helpers
{
    public class MutationBase<TEntity, TEntityDTO, TCreatInput, TUpdateInput> :
        IMutationBase<TEntity, TEntityDTO, TCreatInput, TUpdateInput>
        where TEntity : BaseEntity
        where TUpdateInput : IUpdateInput
    {
        private readonly IMapper mapper;

        public MutationBase(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task<TEntityDTO> Create(AppDbContext dbContext, TCreatInput input)
        {
            var repository = new Repository<TEntity>(dbContext);
            var entity = mapper.Map<TEntity>(input);
            var createdEntity = await repository.Add(entity);
            var entityDto = mapper.Map<TEntityDTO>(createdEntity);

            return entityDto;
        }

        public async Task<TEntityDTO> Update(AppDbContext dbContext, TUpdateInput input)
        {
            var entity = await dbContext.Set<TEntity>()
                .Where(x => x.Id == input.Id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                throw QueryExceptionBuilder.NotFound<TEntity>(input.Id);
            }

            var repository = new Repository<TEntity>(dbContext);
            var entityToUpdate = mapper.Map(input, entity);
            var updatedEntity = await repository.Update(entityToUpdate);
            var entityDto = mapper.Map<TEntityDTO>(updatedEntity);

            return entityDto;
        }

        public async Task<TEntityDTO> Delete(AppDbContext dbContext, int id)
        {
            var repository = new Repository<TEntity>(dbContext);
            var entity = await repository.Get(id);

            if (entity == null)
            {
                throw QueryExceptionBuilder.NotFound<TEntity>(id);
            }

            var entityDeleted = await repository.Delete(id);
            var entityDto = mapper.Map<TEntityDTO>(entityDeleted);

            return entityDto;
        }
    }
}
