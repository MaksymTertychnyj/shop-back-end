using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Services.Interfaces
{
    public interface IEntityService<TEntity>
        where TEntity: class
    {
        Task<IEnumerable<TEntity>> GetAllEntitiesAsync();
        Task<TEntity> GetEntityByKeyAsync(object entityKey);
        Task<TEntity> AddEntityAsync(TEntity entity);
        Task<TEntity> UpdateEntityAsync(TEntity entity, object entityKey);
        Task DeleteEntityAsync(TEntity entity);
    }
}
