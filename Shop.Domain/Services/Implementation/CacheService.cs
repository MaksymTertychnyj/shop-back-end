using Microsoft.Extensions.Caching.Memory;
using Shop.Data.Entities;
using Shop.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Services.Implementation
{
    public class CacheService<TEntity> : ICacheService<TEntity>
        where TEntity : class
    {
        private readonly IMemoryCache memoryCache;
        private readonly IEntityService<TEntity> entityService;

        public CacheService(IMemoryCache memoryCache, IEntityService<TEntity> entityService)
        {
            this.memoryCache = memoryCache;
            this.entityService = entityService;
        }

        public async Task<IEnumerable<TEntity>> GetEntitiesAsync()
        {
            if (!memoryCache.TryGetValue(typeof(TEntity).Name, out IEnumerable<TEntity> values))
            {
                values = await entityService.GetAllEntitiesAsync();
                await Task.Run(() => memoryCache.Set(typeof(TEntity).Name, values));
            }

            return values;
        }

        public async Task UpdateEntitiesAsync()
        {
            var values = await entityService.GetAllEntitiesAsync();
            await Task.Run(() => memoryCache.Remove(typeof(TEntity).Name));
            await Task.Run(() => memoryCache.Set(typeof(TEntity).Name, values));
        }
    }
}
