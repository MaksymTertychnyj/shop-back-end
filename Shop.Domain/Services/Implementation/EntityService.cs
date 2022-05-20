using AutoMapper;
using Shop.Data.Infrastructure;
using Shop.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Services.Implementation
{
    public sealed class EntityService<TEntity> : IEntityService<TEntity>
        where TEntity : class
    {
        private readonly IRepository<TEntity> repository;
        private readonly IMapper mapper;
        

        public EntityService(IRepository<TEntity> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
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
