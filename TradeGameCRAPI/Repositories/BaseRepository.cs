using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeGameCRAPI.Contexts;
using TradeGameCRAPI.Entities;
using TradeGameCRAPI.Models;

namespace TradeGameCRAPI.Repositories
{
    public abstract class BaseRepository<TEntity, TDbContext> : IBaseRepository<TEntity>
        where TEntity : BaseEntity
        where TDbContext : BaseAppDbContext
    {
        private readonly TDbContext dbContext;

        public BaseRepository(TDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            dbContext.Set<TEntity>().Add(entity);

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

        public async Task<TEntity> Get(int id)
        {
            return await dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> FirstOrDefaultAsync(int id)
        {
            return await dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            dbContext.Update(entity);

            await dbContext.SaveChangesAsync();

            return entity;
        }

        public Task SaveChangesAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
