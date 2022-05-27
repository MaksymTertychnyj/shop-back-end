using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Services.Interfaces
{
    public interface IEntityService<TEntity>
        where TEntity: class
    {
        Task<IEnumerable<TEntity>> GetAllEntitiesAsync();
        Task<IEnumerable<TEntity>> GetEntitiesByPropertyAsync(Expression<Func<TEntity, bool>> predicat);
        Task<TEntity> GetEntityByKeyAsync(object entityKey);
        Task<TEntity> AddEntityAsync(TEntity entity);
        Task<TEntity> UpdateEntityAsync(TEntity entity, object entityKey);
        Task DeleteEntityAsync(TEntity entity);
    }
}
