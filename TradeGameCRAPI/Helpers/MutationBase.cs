using AutoMapper;
using HotChocolate;
using HotChocolate.Execution;
using System.Threading.Tasks;
using TradeGameCRAPI.Contexts;
using TradeGameCRAPI.Interfaces;

namespace TradeGameCRAPI.Helpers
{
    public class MutationBase<TEntity, TEntityDTO, TCreatInput, TUpdateInput>
        where TEntity : class
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
            var repository = new Repository<TEntity>(dbContext);
            var entity = await repository.Get(input.Id);

            if (entity == null)
            {
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage($"{typeof(TEntity).Name} with the Id {input.Id} not exist")
                        .SetCode("NOT_EXIST")
                        .Build());
            }

            var entityToUpdate = mapper.Map<TEntity>(input);
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
                throw new QueryException(
                    ErrorBuilder.New()
                        .SetMessage($"{typeof(TEntity).Name} with the Id {id} not exist")
                        .SetCode("NOT_EXIST")
                        .Build());
            }

            var entityDeleted = await repository.Delete(id);
            var entityDto = mapper.Map<TEntityDTO>(entityDeleted);

            return entityDto;
        }
    }
}
