using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeGameCRAPI.Interfaces;
using TradeGameCRAPI.Models;

namespace TradeGameCRAPI.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext dbContext;

        public Repository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<TEntity>> GetAll(bool track = false)
        {
            if (track)
            {
                return await dbContext.Set<TEntity>().ToListAsync();
            }

            return await dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<List<TEntity>> GetByPagination(PaginationDTO paginationDto, bool track = false)
        {
            var queryable = dbContext.Set<TEntity>().AsQueryable();
            var entitiesQuery = queryable
                .Skip((paginationDto.page - 1) * paginationDto.PageSize)
                .Take(paginationDto.PageSize);

            if (track)
            {
                return await entitiesQuery
                    .ToListAsync();
            }

            return await entitiesQuery
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<TEntity> Get(int id)
        {
            return await dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            dbContext.Set<TEntity>().Add(entity);

            await dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            dbContext.Update(entity);

            await dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> Delete(int id)
        {
            var entity = await dbContext.Set<TEntity>().FindAsync(id);

            if (entity == null)
            {
                return entity;
            }

            dbContext.Set<TEntity>().Remove(entity);

            await dbContext.SaveChangesAsync();

            return entity;
        }

        public async void SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task<double> GetCount()
        {
            var count = await dbContext.Set<TEntity>().CountAsync();

            return count;
        }
    }
}
