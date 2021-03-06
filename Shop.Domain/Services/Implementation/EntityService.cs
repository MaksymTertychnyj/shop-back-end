using AutoMapper;
using Shop.Data.Entities;
using Shop.Data.Infrastructure;
using Shop.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Services.Implementation
{
    public sealed class EntityService<TEntity> : IEntityService<TEntity>
        where TEntity : class
    {
        private readonly IRepository<TEntity> repository;
        

        public EntityService(IRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<TEntity>> GetEntitiesByPropertyAsync(Expression<Func<TEntity, bool>> predicat)
        {
            var result = await Task.Run(() => repository.Query().Where(predicat.Compile()));
            return result;
        }

        public async Task<IEnumerable<TEntity>> GetWithIncludedEntities(Expression<Func<TEntity, bool>>? predicat, params Expression<Func<TEntity, object>>[] includes)
        {
            var result = await Task.Run(() => repository.Query(includes));
            if (predicat != null)
            {
                return result.Where(predicat.Compile());
            }

            return result;
        }

        public async Task<TEntity> AddEntityAsync(TEntity entity)
        {
            await repository.AddAsync(entity);

            try
            {
                await repository.SaveChangesAsync();
            }
            catch (Exception)
            {
                return null!;
            }
                
                return entity;
        }

        public async Task DeleteEntityAsync(TEntity entity)
        {
            repository.Delete(entity);
            await repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllEntitiesAsync()
        {
            return await repository.GetAllAsync();
        }

        public async Task<TEntity> GetEntityByKeyAsync(object entityKey)
        {
            TEntity? entityObj = await repository.GetByIdAsync(entityKey);

            if (entityObj != null)
            {
                return entityObj;
            }
            return null!;
            
        }

        public async Task<TEntity> UpdateEntityAsync(TEntity entity, object entityKey)
        {
            TEntity? entityObj = await repository.GetByIdAsync(entityKey);

            if (entityObj != null)
            {
                repository.Delete(entityObj);
                await repository.UpdateAsync(entity);

                try
                {
                    await repository.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return null!;
                }

                return entity;
            }

            return null!;
        }
    }
}
