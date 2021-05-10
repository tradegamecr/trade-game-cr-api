using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeGameCRAPI.Repositories;

namespace TradeGameCRAPI.Services
{
    public abstract class BaseControllerService<
        TEntity,
        TEntityDTO,
        TEntityCreateDTO,
        TEntityUpdateDTO,
        TRepository
    > : IBaseControllerService<TEntity, TEntityDTO, TEntityCreateDTO, TEntityUpdateDTO, TRepository>
        where TEntity : class
        where TEntityDTO : class
        where TEntityCreateDTO : class
        where TEntityUpdateDTO : class
        where TRepository : IBaseRepository<TEntity>
    {
        private TRepository repository;
        private IMapper mapper;

        public BaseControllerService(TRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<TEntityDTO>> GetAll()
        {
            var entities = await repository.GetAll();
            var entitiesDtos = mapper.Map<List<TEntityDTO>>(entities);

            return entitiesDtos;
        }

        public async Task<TEntityDTO> Get(int id)
        {
            var entity = await repository.Get(id);

            if (entity == null)
            {
                return null;
            }

            var entityDto = mapper.Map<TEntityDTO>(entity);

            return entityDto;
        }

        public async Task<TEntity> FirstOrDefaultAsync(int id)
        {
            return await repository.FirstOrDefaultAsync(id);
        }

        public async Task<TEntityDTO> Add(TEntityCreateDTO createEntity)
        {
            var entity = mapper.Map<TEntity>(createEntity);
            var newEntity = await repository.Add(entity);

            return mapper.Map<TEntityDTO>(newEntity);
        }

        public async Task<TEntityDTO> Update(TEntityUpdateDTO updateEntity)
        {
            var entity = mapper.Map<TEntity>(updateEntity);
            var entityUpdated = await repository.Update(entity);

            return mapper.Map<TEntityDTO>(entityUpdated);
        }

        public async Task<TEntityDTO> Delete(int id)
        {
            var entity = await repository.Delete(id);

            return mapper.Map<TEntityDTO>(entity);
        }

        public Task SaveChangesAsync()
        {
            return repository.SaveChangesAsync();
        }

        public TEntityDTO ToEntityDTO(TEntity entity)
        {
            return mapper.Map<TEntityDTO>(entity);
        }

        public void MapReverse(TEntityDTO entityDto, TEntity entity)
        {
            mapper.Map(entityDto, entity);
        }
    }
}
