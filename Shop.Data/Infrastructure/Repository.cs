using Microsoft.EntityFrameworkCore;
using Shop.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Infrastructure
{
    public sealed class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly ShopContext context;
        private readonly DbSet<TEntity> dbEntities;

        public Repository(ShopContext context)
        {
            this.context = context;
            dbEntities = this.context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            return (await dbEntities.AddAsync(entity)).Entity;
        }

        public async Task AddEntitiesAsync(TEntity[] entities)
        {
            await dbEntities.AddRangeAsync(entities);
        }

        public void Delete(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbEntities.ToListAsync();
        }

        public async ValueTask<TEntity?> GetByIdAsync(object key)
        {
            return await dbEntities.FindAsync(key);
        }

        public IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes)
        {
            var dbSet = context.Set<TEntity>();
            var query = includes
                .Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>(dbSet, (current, include) => current.Include(include));

            return query ?? dbSet;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await Task.Run(() => dbEntities.Update(entity).Entity);
        }

        public void Detach(TEntity entity) => context.Entry(entity).State = EntityState.Detached;
    }
}
