using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Infrastructure
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes);

        ValueTask<TEntity?> GetByIdAsync(object key);

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        void Delete(TEntity entity);

        Task<int> SaveChangesAsync();

    }
}
